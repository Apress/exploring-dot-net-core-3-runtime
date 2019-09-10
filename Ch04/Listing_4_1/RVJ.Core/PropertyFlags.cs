#region Namespaces
using System;
using System.Reflection;
#endregion

namespace RVJ.Core {

	[Flags]
	public enum PropertyFlags {

		Private = MethodAttributes.SpecialName | MethodAttributes.Private | MethodAttributes.HideBySig,
		Public = MethodAttributes.SpecialName | MethodAttributes.Public | MethodAttributes.HideBySig,
		Static = MethodAttributes.SpecialName | MethodAttributes.Static,
		WriteOnly = MethodAttributes.SpecialName | MethodAttributes.HideBySig | 100,
		ReadOnly = MethodAttributes.SpecialName | MethodAttributes.HideBySig | 200,
		ReadAndWrite = MethodAttributes.SpecialName | MethodAttributes.HideBySig | 300

	}

};