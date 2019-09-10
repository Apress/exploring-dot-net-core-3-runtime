#region Namespaces
using System;
using System.Reflection;
#endregion

namespace RVJ.Core {

	public class ModuleInfoView : System.Object, IModuleInfoView {

		#region Fields
		private Module _manifestModule;
		private String _name;
		private String _fullyQualifiedName;
		private String _versionID;
		private Guid _versionIDAsGUID;
		private String _metadataVersion;
		private String _PEKindName;
		private String _imageFileMachineName;
		private Assembly _assembly;
		#endregion

		#region Constructors
		/* 
         */
		public ModuleInfoView( Module moduleSource ) {

			if ( moduleSource != null ) {

				this._FillWithModuleInfo( moduleSource );

			};

			return;
		}
		#endregion

		#region Get information about the module when informed the assembly.
		private void _FillWithModuleInfo( Module module ) {

			this._assembly = module.Assembly;
			this._name = module.Name;
			this._fullyQualifiedName = module.FullyQualifiedName;
			this._versionIDAsGUID = module.ModuleVersionId;
			this._versionID = this._versionIDAsGUID.ToString();
			this._metadataVersion = module.MDStreamVersion.ToString();

			PortableExecutableKinds PEKinds;
			ImageFileMachine imageFileMachine;
			module.GetPEKind( out PEKinds, out imageFileMachine );

			this._PEKindName = PEKinds.ToString();
			this._imageFileMachineName = imageFileMachine.ToString();

			if ( this._versionIDAsGUID == this._assembly.ManifestModule.ModuleVersionId )
				this._manifestModule = module;

			return;
		}
		#endregion

		#region Override ToString() method.
		/*
         * Return the fully qualified name of the module.
         */
		public override String ToString() {

			return this._fullyQualifiedName;
		}
		#endregion

		#region Override GetHashCode() method.
		public override Int32 GetHashCode() {
			return base.GetHashCode();
		}
		#endregion

		#region Override Equals method.
		/*
         * Currently, the implementation is using the MVID, a GUID that is also used by the debugger as a way of identifying the .NET Module.
         */
		public override Boolean Equals( Object obj ) {

			return ( Object.ReferenceEquals( this, obj ) || this.Equals( ( obj as IModuleInfoView ) ) );

		}
		#endregion

		#region Overrides IEquatable<IModuleInfoView>.Equals( IModuleInfoView obj ).
		Boolean IEquatable<IModuleInfoView>.Equals( IModuleInfoView other ) {
			return ( ( other != null ) && ( this.VersionIDAsGUID.Equals( other.VersionIDAsGUID ) ) );
		}
		#endregion

		#region Implementation of operators
		public static Boolean operator ==( ModuleInfoView first, ModuleInfoView other ) {
			return ( ( first != null ) && first.Equals( other ) );
		}

		public static Boolean operator !=( ModuleInfoView first, ModuleInfoView other ) {
			return !( ( first != null ) && ( first == other ) );
		}
		#endregion

		#region Properties

		public Assembly Assembly {
			get {

				return this._assembly;

			}
		}

		public String ImageFileMachineName {

			get {

				return this._imageFileMachineName;

			}

		}

		public String Name {
			get {

				return this._name;
			}
		}

		public String FullyQualifiedName {

			get {

				return this._fullyQualifiedName;
			}

		}

		public String MetadataVersion {

			get {

				return this._metadataVersion;
			}

		}

		public String PEKindName {

			get {

				return this._PEKindName;

			}

		}
		public String VersionID {
			get {

				return this._versionID;

			}
		}

		public Guid VersionIDAsGUID {

			get {

				return this._versionIDAsGUID;

			}
		}

		public Module ManifestModule {

			get {

				return this._manifestModule;

			}
			set {

				this._manifestModule = value;
			}

		}


		public Boolean IsManifestModule {

			get {

				return ( this._manifestModule != null );

			}

		}

		#endregion
	};

};
