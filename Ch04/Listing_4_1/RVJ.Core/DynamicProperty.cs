#region Namespaces
using System;
using System.Reflection;
using System.Reflection.Emit;
#endregion

namespace RVJ.Core {
	public class DynamicProperty : IDynamicProperty {

		#region Private Fields
		private readonly TypeBuilder _typeBuilder;
		private readonly FieldBuilder _fieldBuilder;
		private readonly PropertyBuilder _propertyBuilder;
		private readonly PropertyAttributes _propertyAttributes;
		private MethodBuilder _getMethodBuilder;
		private MethodBuilder _setMethodBuilder;
		private const String _Get_Method_Prefix = "get_";
		private const String _Set_Method_Prefix = "set_";
		#endregion

		#region Constructors
		protected DynamicProperty() : base() {
			return;
		}

		protected DynamicProperty( TypeBuilder builder, FieldBuilder fieldBuilder ) : this() {
			this._typeBuilder  = builder;
			this._fieldBuilder = fieldBuilder;
			return;
		}

		protected internal DynamicProperty( String propertyName, Type propertyBaseType, PropertyFlags flags, FieldBuilder fieldBuilder, TypeBuilder typeBuilder ) : this( typeBuilder, fieldBuilder ) {

			/*
			
			The TypeBuilder.DefineProperty instance method is used for definition of a dynamic .NET Property.
			The PropertyAttributes enum is a group of flags that can be combined to define characteristics of a dynamic .NET Property. 
			For example, the PropertyAttributes.HasDefault enum option defines that the underlying field associated with the property should have a default value.
			
			*/

			this._propertyBuilder = this._typeBuilder.DefineProperty( propertyName,
				PropertyAttributes.HasDefault,
				propertyBaseType, null );

			this._propertyAttributes = ( PropertyAttributes ) flags;

			return;
		}

		#endregion

		#region Private properties
		private TypeBuilder TypeBuilder {

			get {

				return this._typeBuilder;
			}

		}

		private FieldBuilder FieldBuilder {
			get {

				return this._fieldBuilder;

			}

		}

		private MethodBuilder GetMethodBuilder {

			get {

				return this._getMethodBuilder;

			}

			set {

				this._getMethodBuilder = value;

			}

		}

		private MethodBuilder SetMethodBuilder {

			get {

				return this._setMethodBuilder;

			}

			set {

				this._setMethodBuilder = value;

			}

		}

		private PropertyBuilder PropertyBuilder {
			get {

				return this._propertyBuilder;

			}
		}
		#endregion

		#region Protected Internal methods
		protected internal void DefineSetMethod() {

			/*

			We should define a set accessor method for our dynamic .NET Property, as this is required by the implementation of our application. 
			A definition of a set accessor method is not required by the metadata system of the platform.

			*/

			MethodBuilder setMethodBuilder = this.SetMethodBuilder;
			PropertyBuilder setPropertyBuilder = this.PropertyBuilder;


			setMethodBuilder = this.TypeBuilder.DefineMethod( setPropertyBuilder.Name.Insert( 0, DynamicProperty._Set_Method_Prefix ), ( MethodAttributes.HideBySig| MethodAttributes.Public  | MethodAttributes.SpecialName ), null, new Type[] { setPropertyBuilder.PropertyType } );


			ILGenerator MSIL_Set_Generator = setMethodBuilder.GetILGenerator();

			MSIL_Set_Generator.Emit( OpCodes.Ldarg_0 );
			MSIL_Set_Generator.Emit( OpCodes.Ldarg_1 );
			MSIL_Set_Generator.Emit( OpCodes.Stfld, this.FieldBuilder );
			MSIL_Set_Generator.Emit( OpCodes.Ret );


			setPropertyBuilder.SetSetMethod( setMethodBuilder );


			return;
		}
		protected internal void DefineGetMethod() {
			/*
			
			We should define a get accessor method for our dynamic .NET Property, as this is required by the implementation of our application. 
			A definition of a get accessor method is not required by the metadata system of the platform.

			*/

			MethodBuilder getMethodBuilder = this.GetMethodBuilder;
			PropertyBuilder getPropertyBuilder = this.PropertyBuilder;

			getMethodBuilder = this.TypeBuilder.DefineMethod( getPropertyBuilder.Name.Insert( 0, DynamicProperty._Get_Method_Prefix ), ( MethodAttributes ) ( MethodAttributes.HideBySig| MethodAttributes.Public  | MethodAttributes.SpecialName ), getPropertyBuilder.PropertyType, Type.EmptyTypes );

			ILGenerator MSIL_Get_Generator = getMethodBuilder.GetILGenerator();

			MSIL_Get_Generator.Emit( OpCodes.Ldarg_0 );
			MSIL_Get_Generator.Emit( OpCodes.Ldfld, this.FieldBuilder );
			MSIL_Get_Generator.Emit( OpCodes.Ret );

			getPropertyBuilder.SetGetMethod( getMethodBuilder );

			return;
		}
		#endregion

	}
};
