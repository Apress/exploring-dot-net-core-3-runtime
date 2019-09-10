#region Namespaces
using System;
using System.CodeDom;
#endregion
namespace RVJ.Core.CodeDOM {

	public class FieldInformation{

		#region Private Fields
		private readonly String _name;
		#endregion

		#region Public Fields
		public readonly CodeTypeReference TypeReference;
		public readonly Type BaseType;
		public readonly TypeCode TypeCode;
		public FieldAttributes Attributes;
		#endregion

		#region Constructors

		private FieldInformation() : base() { }

		public FieldInformation( String name, TypeCode baseTypeCode ): this() {

			if ( !String.IsNullOrEmpty( name ) && !String.IsNullOrWhiteSpace( name ) ) {

				this._name = name;
				this.TypeCode = baseTypeCode;

				this.__SetCodeTypeReference( baseTypeCode, ref this.TypeReference );

				this.BaseType = Type.GetType( this.TypeReference.BaseType );

			}


			return;
		}

		public FieldInformation( String name, Type baseType ) : this() {

			if ( ( !String.IsNullOrEmpty( name ) && ( !String.IsNullOrWhiteSpace( name ) && ( baseType != null ) ) ) ) {

				this.__SetCodeTypeReference( baseType, ref this.TypeReference );

				this.BaseType = Type.GetType( this.TypeReference.BaseType );
			};

			return;
		}
		#endregion

		#region Private Behaviors

		private void __SetCodeTypeReference( TypeCode typeCode, ref CodeTypeReference typeReference ) {

			Type baseType;

			switch ( typeCode ) {

				case TypeCode.String: {

					baseType = typeof( String );

					break;
				};

				case TypeCode.Int32: {

					baseType = typeof( Int32 );

					break;
				};

				case TypeCode.UInt32: {

					baseType = typeof( UInt32 );

					break;
				};

				default: {

					baseType = typeof( Object );

					break;
				};
			};


			typeReference = new CodeTypeReference( baseType );


			return;
		}


		private void __SetCodeTypeReference( Type baseType, ref CodeTypeReference typeReference ) {

			if(  ( baseType != null ) && ( typeReference != null ) ) typeReference = new CodeTypeReference( baseType );

			typeReference = new CodeTypeReference( baseType );

			return;
		}
		#endregion

			#region Public Properties
		public String Name { get { return this._name;  } }
		#endregion

	};

};