#region Namespaces
using System;
using System.Reflection;
using System.Reflection.Emit;
#endregion

namespace RVJ.Core {
	public class DynamicBuilder : IDynamicBuilder {

		#region Private Fields
		private AssemblyBuilder _assemblyBuilder;
		private AssemblyName _assemblyName;
		#endregion

		#region Constructors
		protected DynamicBuilder() {

		}
		public DynamicBuilder( String nameOfAssembly ) : this() {

			/*  We need a name for an assembly. */

			this._assemblyName = new AssemblyName( nameOfAssembly );

		}
		#endregion
		public IDynamicAssembly DefineDynamicAssembly() {


			/* 
			 * The  AssemblyBuilder is used for definition of a dynamic assembly.
			 * We need to inform the assembly name.
			 * 
			 * The AssemblyBuilder does not have a public constructor, so we need to use the AssemblyBuilder.DefineDynamicAssembly() static method for the definition of a dynamic assembly:
			 * 
			 * 
			 *  AssemblyBuilder assemblyBuilder = new AssemblyBuilder(); 
			 *  
			 * Currently, there is no method for save a dynamic assembly on disk.
			 */
			this._assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly( this._assemblyName,
				AssemblyBuilderAccess.RunAndCollect );


			return new DynamicAssembly( this._assemblyBuilder );
		}

		#region Protected Internal Methods
		protected internal AssemblyBuilder _getAssemblyBuilder() {

			return this._assemblyBuilder;

		}
		#endregion


	};
};
