#region Namespaces
using System.Reflection.Emit;
#endregion

namespace RVJ.Core {
	public class DynamicAssembly : IDynamicAssembly {

		#region Private Fields
		private AssemblyBuilder _assemblyBuilder;
		#endregion

		#region Constructors
		protected DynamicAssembly() {

		}

		public DynamicAssembly( AssemblyBuilder builder ) {
			this._assemblyBuilder  = builder;
		}

		#endregion

		#region Protected Internal Methods
		protected internal AssemblyBuilder _getAssemblyBuilder() {
			return this._assemblyBuilder;
		}
		#endregion

		#region Public Methods
		public IDynamicModule DefineDynamicModule() {
			/*

			Defines and returns a dynamic .NET Module and associate it with the dynamic .NET Assembly.
			The name used for the assembly is the same used for the module.

			*/

			return new DynamicModule( this._assemblyBuilder.DefineDynamicModule(
				this._assemblyBuilder.GetName().Name ),
				this._assemblyBuilder );

		}
		#endregion

	}
};
