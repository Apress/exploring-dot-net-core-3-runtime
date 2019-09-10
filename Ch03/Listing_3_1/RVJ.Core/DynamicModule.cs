#region Namespaces
using System;
using System.Reflection;
using System.Reflection.Emit;
#endregion

namespace RVJ.Core {
	public class DynamicModule : IDynamicModule {

		#region Private Fields
		private ModuleBuilder _moduleBuilder;
		private AssemblyBuilder _assemblyBuilder;
		#endregion

		#region Constructors
		protected DynamicModule() {

		}

		public DynamicModule( ModuleBuilder builder, AssemblyBuilder assembly ) {
			this._moduleBuilder  = builder;
			this._assemblyBuilder = assembly;
		}

		#endregion

		#region Protected Internal Methods
		protected internal ModuleBuilder _getModuleBuilder() {
			return this._moduleBuilder;
		}

		protected internal AssemblyBuilder _getAssemblyBuilder() {
			return this._assemblyBuilder;
		}
		#endregion

		#region Public Methods
		public IDynamicType DefineDynamicType( String nameOfType, ClassTypeFlags flags, Type directAncestor ) {

			return new DynamicType(
				this._moduleBuilder.DefineType( nameOfType,
				( ( TypeAttributes ) flags ),
				directAncestor ),
				this._moduleBuilder );

		}
		#endregion


		#region Public Properties
		#endregion

	};
};
