#region Namespaces
using System;
using System.Collections.Generic;
#endregion

namespace RVJ.Core.CodeDOM {

	public class  AssemblyInformation {

		#region Private fields
		private readonly String _name;
		private readonly String[] _imports; /* List of namespaces used by this type */
		private readonly List<TypeInformation> _types;
		#endregion

		#region Constructors
		private AssemblyInformation() : base() {

			this._types = new List<TypeInformation>();

			return;
		}

		public AssemblyInformation( String name, String[] imports ): this() {

			if ( !String.IsNullOrEmpty( name ) && !String.IsNullOrWhiteSpace( name ) ) this._name = name;
			if ( ( imports != null ) && ( imports.Length > 0 ) ) this._imports = imports;

				return;
		}
		#endregion



		#region Public Behaviors

		public void AddType( TypeInformation newType ) {

			if ( newType != null ) this._types.Add( newType );

			return;
		}

		#endregion

		#region Propeties

		public String Name {  get { return this._name; } }

		public String[] Imports { get { return this._imports; } }

		public TypeInformation[] Types { get { return this._types.ToArray(); } }

		#endregion
	}

};