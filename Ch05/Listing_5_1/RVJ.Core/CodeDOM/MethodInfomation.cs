#region Namespaces
using System;
using System.Collections.Generic;
#endregion
namespace RVJ.Core.CodeDOM {

	public class MethodInformation {

		#region Private fields
		private readonly String _name;
		#endregion

		#region Constructors
		private MethodInformation() { return;  }

		public MethodInformation( String name ): base() {

			if ( !String.IsNullOrEmpty( name ) && !String.IsNullOrWhiteSpace( name ) ) this._name = name;


			return;
		}
		#endregion

		#region Properties
		public String Name {  get { return this._name;  } }
		#endregion

	}

};