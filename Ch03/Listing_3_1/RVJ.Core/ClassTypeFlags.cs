#region Namespaces
using System.Reflection;
#endregion

namespace RVJ.Core {

	public enum ClassTypeFlags {
		Private = ( TypeAttributes.NotPublic | TypeAttributes.Class ),
		Public = ( TypeAttributes.Public | TypeAttributes.Class ),
		Abstract = ( TypeAttributes.Abstract | TypeAttributes.Class )

	}

};