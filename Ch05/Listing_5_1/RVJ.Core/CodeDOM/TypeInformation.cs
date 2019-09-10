#region Namespaces
using System;
using System.Collections.Generic;
#endregion
namespace RVJ.Core.CodeDOM {

	public class TypeInformation {

		#region Private Fields

		private readonly String _name;
		private readonly String _namespace;
		private readonly String _fullName;
		private readonly List<FieldInformation> _fields;
		private readonly List<PropertyInformation> _properties;
		private readonly List<MethodInformation> _methods;
		private readonly List<InterfaceInformation> _interfaces;

		#endregion

		#region Constructors

		private TypeInformation() : base() {

			this._fields = new List<FieldInformation>();
			this._interfaces = new List<InterfaceInformation>();
			this._methods = new List<MethodInformation>();
			this._properties = new List<PropertyInformation>();

			return;
		}

		public TypeInformation( String name, String namespaceOfType ) : this() {

			if ( !String.IsNullOrEmpty( name ) && !String.IsNullOrWhiteSpace( name ) ) {

				if ( !String.IsNullOrEmpty( namespaceOfType ) && !String.IsNullOrWhiteSpace( namespaceOfType ) ) {

					this._name = name;
					this._namespace = namespaceOfType;

					this._fullName = this._namespace + "." + this._name;

				}
			}



			return;
		}
		#endregion

		#region Public Behaviors
		public void AddField( FieldInformation newField ) {

			if ( newField != null ) this._fields.Add( newField );

			return;
		}

		public void AddFields( FieldInformation[] newFields ) {

			if ( ( newFields != null ) && ( newFields.Length > 0 ) ) this._fields.AddRange( newFields );

			return;
		}
		#endregion

		#region Public Properties
		public TypeClassification Classification { get; set; }

		public String Namespace {  get { return this._namespace; }  }

		public String Name {  get { return this._name; } }

		public String FullName {  get { return this._fullName; } }

		public FieldInformation[] Fields {

			get {

				return this._fields.ToArray();

			}
		}

		public PropertyInformation[] Properties {

			get {

				return this._properties.ToArray();

			}
		}

		public MethodInformation[] Methods {

			get {

				return this._methods.ToArray();

			}
		}

		public InterfaceInformation[] Interfaces {

			get {

				return this._interfaces.ToArray();

			}

		}
		#endregion

	}

};