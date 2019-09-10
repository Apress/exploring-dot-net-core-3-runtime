#region Namespaces
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
#endregion

namespace RVJ.Core {
	public class DynamicType : IDynamicType {

		#region Private Fields
		private TypeBuilder _typeBuilder;
		private ModuleBuilder _moduleBuilder;
		private List<IDynamicField> _dynamicFields;
		private Boolean _isTypeCreated;
		private Type _typeCreated;
		#endregion

		#region Constructors
		protected DynamicType() : base() {
			this._dynamicFields = new List<IDynamicField>();
		}

		public DynamicType( TypeBuilder builder, ModuleBuilder module ) : this() {
			this._typeBuilder  = builder;
			this._moduleBuilder = module;

		}

		#endregion

		#region Protected Internal Methods
		protected internal TypeBuilder _getTypeBuilder() {
			return this._typeBuilder;
		}

		protected internal ModuleBuilder _getModuleBuilder() {
			return this._moduleBuilder;
		}
		#endregion

		#region Public Methods

		public IDynamicField DefineDynamicField( String fieldName, Type fieldType, FieldFlags flags ) {
			return this.AddField( fieldName, fieldType, flags, this._typeBuilder );
		}
		private IDynamicField AddField( String fieldName, Type fieldType, FieldFlags flags, TypeBuilder builder ) {

			IDynamicField local = new DynamicField( fieldName, fieldType, flags, builder );

			this._dynamicFields.Add( local );

			return local;
		}


		public Object CreateAnInstance( String dynamicTypeName ) {
			if ( !this._isTypeCreated ) {
				this._typeCreated = this._typeBuilder.CreateType();
				this._isTypeCreated = true;
			}
			return Activator.CreateInstance( this._typeCreated );
		}
		#endregion
	}
};
