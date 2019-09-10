#region Namespaces
using System;
#endregion

namespace RVJ.Core {
	public interface IDynamicModule {

		IDynamicType DefineDynamicType( String nameOfType, ClassTypeFlags flags, Type directAncestor );

	}
}
