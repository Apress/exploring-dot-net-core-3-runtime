#region Namespaces
using System;
#endregion

namespace RVJ.Core {
	public interface IDynamicField {

		IDynamicProperty DefineDynamicProperty( String propertyName, Type propertyType, PropertyFlags flags );

	};
};
