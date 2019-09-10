#region Namespaces
using System;
using System.Reflection;
#endregion

namespace RVJ.Core {

	[Flags]
	public enum ClassTypeFlags {
		Private = ( TypeAttributes.NotPublic | TypeAttributes.Class ),
		Public = ( TypeAttributes.Public | TypeAttributes.Class ),
		Abstract = ( TypeAttributes.Abstract | TypeAttributes.Class )

	}

};