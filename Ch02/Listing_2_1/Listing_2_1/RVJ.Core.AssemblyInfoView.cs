
#region Namespaces
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
#endregion

namespace RVJ.Core {

	public class AssemblyInfoView : System.Object, IAssemblyInfoView {

		#region private fields
		private readonly StringBuilder _buffer;
		private Assembly _assembly;
		private AssemblyName _assemblyName;
		private Version _version;
		private Module _manifestModule;
		private String _imageFileMachineName;
		private String _PEKindName;
		private readonly List<TypeInfo> _definedTypes;
		private readonly List<Module> _modules;
		private readonly List<IModuleInfoView> _moduleInfoViewCollection;
		#endregion



		#region Private behaviors
		private void _FillWithAssemblyInfoForAsync( TypeInfo type ) {


			/*

			https://github.com/dotnet/coreclr/pull/9491/commits/49d2d8f711286bf4ab3ffcd7b9f89a2ec80725bd

			=> Avoid the use of Assembly.GetExecutingAssembly(). 

			The Assembly.GetExecutingAssembly() is no inlined because of the way that it was implemented. So we have a loss on performance.

			=> Typically, to get a reference to the instance of the assembly in execution, using the following:

			Assembly assembly = typeof( Program ).Assembly;

			Better yet, since .NET Framework 4.5 and .NET Core 1.0, we have the System.Reflection.TypeInfo abstract .net class that is is based on System.Type and System.Reflection.IReflectableType. This new types includes a series of improvements that are explained in the book material.

			public abstract class TypeInfo : Type, System.Reflection.IReflectableType{...}

			*/

			if ( type == null )
				type = IntrospectionExtensions.GetTypeInfo( typeof( RVJ.Core.AssemblyInfoView ) );

			this._assembly = IntrospectionExtensions.GetTypeInfo( type ).Assembly;

			this._assemblyName = this._assembly.GetName();
			this._version = this._assemblyName.Version;

			this._manifestModule = this._assembly.ManifestModule;

			PortableExecutableKinds PEKinds;
			ImageFileMachine imageFileMachine;
			this._manifestModule.GetPEKind( out PEKinds, out imageFileMachine );


			this._PEKindName = PEKinds.ToString();
			this._imageFileMachineName = imageFileMachine.ToString();

			this._FillWithInfoOfDefinedTypesForAsync( this._assembly );

			/* 
             * Add all modules to the module list. The ModuleInfoView instances of collection will be filled using the information in these instances of Module, referenced by this._modules list.
             */
			this._manifestModule = this._assembly.ManifestModule;

			/*
             * Check for not duplicate manifest module information.
             * If collection of modules already has the instance of the manifest module, start from the second module in the colletion returned by GetModules() method.
             */

			this._modules.AddRange( this._assembly.GetModules() );

			foreach ( Module module in this._modules ) {
				IModuleInfoView item = new ModuleInfoView( module );
				this._moduleInfoViewCollection.Add( item );
			}


			return;
		}


		private void _FillWithInfoOfDefinedTypesForAsync( Assembly assembly ) {


			/*

			When we are using the Reflection technology, loop operations should be locked to avoid more than one thread to access the same collection at the same time, and consequently, the same group of data pointed by that collection. This locking technique have the purpose of avoid corruption of data and benefits the performance.

			The first step should be getting the enumerator to the collection, and after that,  is important to lock the collection before do the loop.
			After finished the loop, we must remove the lock for the respective collection and thread.

			*/

			{

				Boolean isLocked = new Boolean();

				using ( IEnumerator<TypeInfo> definedTypes = assembly.DefinedTypes.GetEnumerator() ) {

					Monitor.Enter( definedTypes, ref isLocked );

					if ( isLocked ) {

						try {

							this._definedTypes.AddRange( assembly.DefinedTypes );

						} finally {
							Monitor.Exit( definedTypes );
						};
					};
				};
			};

			return;
		}

		#endregion

		#region Public constructors
		public AssemblyInfoView() {

			this._buffer = new StringBuilder();
			this._definedTypes = new List<TypeInfo>();
			this._modules = new List<Module>();
			this._moduleInfoViewCollection = new List<IModuleInfoView>();

			return;
		}
		#endregion

		#region Public Properties

		public String FullName {
			get {

				return this._assembly.FullName;

			}
		}
		public String EntryPointMethodName {
			get {

				return ( ( this._assembly.EntryPoint != null ) ? this._assembly.EntryPoint.Name : "(null)" );

			}
		}
		public String Location {
			get {

				return this._assembly.Location;

			}
		}
		public Boolean IsPublishedOnGAC {
			get {

				return this._assembly.GlobalAssemblyCache;

			}
		}

		public Boolean IsFullyTrusted {
			get {

				return this._assembly.IsFullyTrusted;

			}
		}

		public Boolean IsReflectionOnly {
			get {

				return this._assembly.ReflectionOnly;

			}
		}
		public String CodeBase {
			get {

				return this._assembly.CodeBase;

			}
		}

		public TypeInfo[] DefinedTypes {
			get {

				return this._definedTypes.ToArray();

			}
		}
		public String ImageRuntimeVersion {
			get {

				return this._assembly.ImageRuntimeVersion;

			}
		}
		public Boolean IsDynamic {

			get {

				return this._assembly.IsDynamic;

			}

		}
		public Boolean IsStatic {
			get {

				return !this._assembly.IsDynamic;

			}
		}

		public Boolean IsMultiModule {

			get {

				return ( this._assembly.GetModules().Length > 0 );

			}

		}
		public String Name {
			get {

				return this._assemblyName.Name;

			}
		}
		public String CultureName {
			get {

				return ( ( this._assemblyName.CultureName.Length == 0 ) ? "neutral" : this._assemblyName.CultureName );

			}
		}

		/*

            Gets or sets the information related to the assembly's compatibility with other assemblies.
            VersionCompatibility information indicates, for example, if the assembly cannot execute side-by-side with other versions due to conflicts over a device driver.

            Currently, VersionCompatibility always returns AssemblyVersionCompatibility.SameMachine, and is not used by the loader. This property is reserved for a future feature.

        */
		public String VersionCompatibility {
			get {

				return this._assemblyName.VersionCompatibility.ToString();

			}
		}
		public String VersionMajorNumber {
			get {

				return this._version.Major.ToString();

			}
		}
		public String VersionMinorNumber {
			get {

				return this._version.Minor.ToString();

			}
		}
		public String VersionBuildNumber {
			get {

				return this._version.Build.ToString();

			}
		}
		public String VersionRevisionNumber {
			get {

				return this._version.Revision.ToString();

			}
		}
		public String VersionMajorRevisionNumber {
			get {

				return this._version.MajorRevision.ToString();

			}
		}
		public String VersionMinorRevisionNumber {
			get {

				return this._version.MinorRevision.ToString();

			}
		}
		public String PublicKeyTokenValue {
			get {

				Byte[] publicKeyToken = this._assemblyName.GetPublicKeyToken();
				return ( publicKeyToken.Length == 0 ? "(null)" : publicKeyToken.ToString() );

			}
		}
		public String ManifestModuleName {
			get {

				return this._manifestModule.Name;

			}
		}

		public String MetadataStreamVersion {
			get {

				return this._manifestModule.MDStreamVersion.ToString();

			}
		}

		public String MetadataToken {
			get {

				return this._manifestModule.MetadataToken.ToString();

			}
		}

		public String PEKindName {
			get {

				return this._PEKindName;

			}
		}

		public String ImageFileMachineName {
			get {

				return this._imageFileMachineName;

			}
		}

		#endregion

		#region Fill with information about an assembly instance.

		public void DoAsync( TypeInfo typeInfo ) {

			Action<Object> action = ( Object type ) => { this._FillWithAssemblyInfoForAsync( ( TypeInfo ) type ); };

			using ( Task taskGetAssemblyInfo = new Task( action, typeInfo ) ) {

				taskGetAssemblyInfo.Start();
				taskGetAssemblyInfo.Wait();

			};

			return;
		}
		#endregion

		#region Override Object.ToString() method
		public override String ToString() {

			String local;

			this._buffer.AppendFormat( "Assembly Full Name (display name): {0}\nAssembly location: {1}\nAssembly Code base: {2}\nCLR version: {3}\nAssembly is static: {4}\nAssembly full name (via ToString()): {5}\nName: {6}\nCultureName: {7}\nVersion.Major: {8}\nVersion.Minor: {9}\nVersion.Build: {10}\nVersion.Revision: {11}\nVersion.MajorRevision: {12}\nVersion.MinorRevision: {13}\nPublic key token: {14}\nManifest Module: {15}\n", this.FullName, this.Location, this.CodeBase, this.ImageRuntimeVersion, this.IsDynamic.ToString(), this.FullName, this.Name, this.CultureName, this.VersionMajorNumber, this.VersionMinorNumber, this.VersionBuildNumber, this.VersionRevisionNumber, this.VersionMajorRevisionNumber, this.VersionMinorRevisionNumber, this.PublicKeyTokenValue, this.ManifestModuleName );

			local = String.Intern( this._buffer.ToString() );
			this._buffer.Clear();

			return local;
		}
		#endregion

		#region Override System.Object.GetHashCode() method.
		public override Int32 GetHashCode() {
			return base.GetHashCode();
		}
		#endregion

	};
};
