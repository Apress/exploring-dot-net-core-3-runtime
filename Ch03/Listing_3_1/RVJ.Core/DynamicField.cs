#region Namespaces
using System;
using System.Reflection;
using System.Reflection.Emit;
#endregion

namespace RVJ.Core {
	public class DynamicField : IDynamicField {

		#region Private Fields
		private FieldBuilder _fieldBuilder;
		private TypeBuilder _typeBuilder;
		#endregion

		#region Constructors
		protected DynamicField() : base() {
			return;
		}

		protected DynamicField( TypeBuilder builder ) : this() {
			this._typeBuilder  = builder;
			return;
		}

		protected internal DynamicField( String fieldName, Type fieldType, FieldFlags flags, TypeBuilder builder ) : this( builder ) {

			this._fieldBuilder = this._typeBuilder.DefineField( fieldName,
				fieldType,
				( ( FieldAttributes ) flags ) );

			return;
		}

		#endregion

		#region Protected Internal Methods
		protected internal TypeBuilder _getTypeBuilder() {
			return this._typeBuilder;
		}

		protected internal FieldBuilder _getFieldBuilder() {
			return this._fieldBuilder;
		}

		#endregion

	}
};
