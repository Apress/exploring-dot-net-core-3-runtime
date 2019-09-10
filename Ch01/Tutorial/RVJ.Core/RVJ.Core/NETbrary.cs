#region Namespaces
using System.Reflection;
using System.Reflection.Emit;
#endregion

namespace RVJ.Core {
	public class NETLibrary {

		static void SampleMethod() {

			AssemblyBuilderAccess builderAccess;

#if NET48

			builderAccess = AssemblyBuilderAccess.ReflectionOnly;

#elif NETCOREAPP3_0

			builderAccess = AssemblyBuilderAccess.RunAndCollect;

#endif
			AssemblyBuilder builder = AssemblyBuilder.DefineDynamicAssembly( new AssemblyName( "RVJ.Core" ), builderAccess );

			return;
		}
	};
};
