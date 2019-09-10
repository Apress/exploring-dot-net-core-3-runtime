#region Namespaces
using System;
using System.Reflection;
#endregion

namespace RVJ.Core {

	[Flags]
	public enum FieldFlags {

		Private = FieldAttributes.Private,
		Public = FieldAttributes.Public,
		Static = FieldAttributes.Static

	}

};