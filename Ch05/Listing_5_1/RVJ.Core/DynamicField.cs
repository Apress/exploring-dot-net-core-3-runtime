#region Namespaces
using System;
using System.Reflection;
using System.Reflection.Emit;
#endregion

namespace RVJ.Core {
	public class DynamicField : IDynamicField {

		#region Private Fields
		private readonly FieldBuilder _fieldBuilder;
		private readonly TypeBuilder _typeBuilder;
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


		#region Private Methods
		private IDynamicProperty AddProperty( String propertyName, Type propertyBaseType, PropertyFlags flags, FieldBuilder fieldBuilder, TypeBuilder builder ) {

			/*
			
			A dynamic .NET Property pertains to a dynamic .NET Type. 
			
			*/


			return new DynamicProperty( propertyName, propertyBaseType, flags, fieldBuilder, builder );
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

		#region Public Methods
		public IDynamicProperty DefineDynamicProperty( String propertyName, Type propertyType, PropertyFlags flags ) {

			DynamicProperty dynamicProperty = ( DynamicProperty ) this.AddProperty( propertyName, propertyType, flags, this._fieldBuilder, this._typeBuilder );

			if ( flags.HasFlag( PropertyFlags.ReadAndWrite ) ) {
				dynamicProperty.DefineGetMethod();
				dynamicProperty.DefineSetMethod();
			} else if ( flags.HasFlag( PropertyFlags.ReadOnly ) ) dynamicProperty.DefineGetMethod();
			else if ( flags.HasFlag( PropertyFlags.WriteOnly ) ) dynamicProperty.DefineSetMethod();

			return dynamicProperty;
		}
		#endregion

	}
};
