#region Namespaces
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Reflection;
#endregion

namespace RVJ.Core {
    public class Program {

        static String[] GetFundamentalTypes() {

            List<String> listOfNames = new List<String>();

            listOfNames.AddRange( new String[] { typeof( System.Boolean ).FullName, typeof( System.Char ).FullName, typeof( System.Object ).FullName, typeof( System.String ).FullName, typeof( System.Single ).FullName, typeof( System.Double ).FullName, typeof( System.SByte ).FullName, typeof( System.Int16 ).FullName, typeof( System.Int32 ).FullName, typeof( System.Int64 ).FullName, typeof( System.IntPtr ).FullName, typeof( System.UIntPtr ).FullName, typeof( System.TypedReference ).FullName, typeof( System.Byte ).FullName, typeof( System.UInt16 ).FullName, typeof( System.UInt32 ).FullName, typeof( UInt64 ).FullName, } );

            listOfNames.Sort();

            return listOfNames.ToArray();
        }

        static void Main() {


            AssemblyInfoView asmInfo = new AssemblyInfoView();

            using ( Task getAssemblyInfo = Task.Run( () => { asmInfo.DoAsync( ( ( TypeInfo ) typeof( STAThreadAttribute ) ) ); return; } ) ) {

                getAssemblyInfo.Wait();

            };

            TypeInfo[] definedTypes = asmInfo.DefinedTypes;
            Console.WriteLine( "{0}\nTotal of defined types for assembly {1} is {2}.\n", asmInfo.ToString(), asmInfo.Name, definedTypes.Length.ToString() );

            asmInfo = null;


            Console.WriteLine( "Press <ENTER> to finish" );
            Console.ReadLine();

            return;
        }
    };
};
