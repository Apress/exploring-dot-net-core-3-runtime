#region Namespaces
using System;
#endregion

namespace RVJ.Core {
	public interface IDynamicType {
		IDynamicField DefineDynamicField( String fieldName, Type fieldType, FieldFlags flags );
		Object CreateAnInstance( String dynamicTypeName );

	}
};
