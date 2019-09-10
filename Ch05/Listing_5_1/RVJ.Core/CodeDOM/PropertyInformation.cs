#region Namespaces
using System;
#endregion
namespace RVJ.Core.CodeDOM {

	public class PropertyInformation {

		#region Private fields
		private readonly String _name;
		#endregion

		#region Constructors
		private PropertyInformation() { return;  }

		public PropertyInformation( String name ): base() {

			if ( !String.IsNullOrEmpty( name ) && !String.IsNullOrWhiteSpace( name ) ) this._name = name;

			return;
		}
		#endregion

		#region Properties
		public String Name { get { return this._name; } }
		#endregion
	}


};