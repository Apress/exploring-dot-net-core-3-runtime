#region Namespaces
using System;
using System.Reflection;
using RVJ.Core;
using RVJ.Core.CodeDOM;
#endregion


namespace Listing_5_1 {
    public class Program {

        static void Pause( Boolean finish = false, Boolean clearConsole = true ) {

            const String PressMessage = "Press <ENTER>";
            const String PauseMessage = " to continue...";
            const String FinishMessage = " to finish...";
            Console.WriteLine( "{0}{1}", PressMessage, ( !finish ? PauseMessage : FinishMessage ) );
            Console.ReadLine();
            if ( clearConsole ) Console.Clear();

            return;

        }

        #region Using RVJ.Core.CodeDOM types
        public static void UsingCodeDOM() {

            CodeGenerator codeGenerator = new CodeGenerator();
            codeGenerator.ProgrammingLanguage = ProgrammingLanguages.CSharp;
            codeGenerator.RootNamespace = "RVJ.Core.Sample";
            codeGenerator.SourceFileName = "Person.cs";

            AssemblyInformation assemblyInfo = new AssemblyInformation( codeGenerator.RootNamespace, new String[] { "System" } );

            codeGenerator.AssemblyInformation = assemblyInfo;

            #region Declares and defines a new type.
            TypeInformation personType = new TypeInformation( "Person", codeGenerator.RootNamespace );

            FieldInformation idField = new FieldInformation( "_id", TypeCode.Int32 );
            FieldInformation nameField = new FieldInformation( "_name", TypeCode.String );
            FieldInformation ageField = new FieldInformation( "_age", TypeCode.Int32 );

            idField.Attributes = RVJ.Core.CodeDOM.FieldAttributes.Private;
            nameField.Attributes = RVJ.Core.CodeDOM.FieldAttributes.Private;
            ageField.Attributes = RVJ.Core.CodeDOM.FieldAttributes.Private;

            personType.AddFields( new FieldInformation[] { idField, nameField, ageField } );



            #endregion

            assemblyInfo.AddType( personType );


            codeGenerator.Generate();
            codeGenerator.Save();

            return;
        }
        #endregion
        static void Main() {


            Program.UsingCodeDOM();

            String assemblyName = "RVJ.Core.Person";
            IDynamicBuilder personBuilder = new DynamicBuilder( assemblyName );


            /*

			Defines a dynamic .NET Assembly.
			The method automatically defines a dynamic .NET Module with the same name.

			 */
            IDynamicAssembly personDynamicAssembly = personBuilder.DefineDynamicAssembly();

            /*

			Defines a dynamic .NET Module.

			*/

            IDynamicModule personDynamicModule = personDynamicAssembly.DefineDynamicModule();

            /*

			Defines a dynamic .NET Type.

			 */

            IDynamicType personDynamicType = personDynamicModule.DefineDynamicType( assemblyName, ClassTypeFlags.Public, typeof( System.Object ) );


            IDynamicField _id_DynamicField = personDynamicType.DefineDynamicField( "_id", typeof( System.UInt32 ), FieldFlags.Private );
            IDynamicField _name_DynamicField = personDynamicType.DefineDynamicField( "_name", typeof( System.String ), FieldFlags.Private );
            IDynamicField _age_DynamicField = personDynamicType.DefineDynamicField( "_age", typeof( System.UInt32 ), FieldFlags.Private );

            /*

			A dynamic .NET Property is associated with a dynamic .NET Field.
			By default, the implementation of IDynamicField.DefineDynamicProperty method defines both, get  and set accessor methods.
			We should use RVJ.Core.PropertyFlags enum option to indicates if characteristics of a dynamic .NET Property.

			*/

            _id_DynamicField.DefineDynamicProperty( "Id", typeof( System.UInt32 ), PropertyFlags.Public | PropertyFlags.ReadAndWrite );
            _name_DynamicField.DefineDynamicProperty( "Name", typeof( System.String ), PropertyFlags.Public | PropertyFlags.ReadAndWrite );
            _age_DynamicField.DefineDynamicProperty( "Age", typeof( System.UInt32 ), PropertyFlags.Public | PropertyFlags.ReadAndWrite );

            /*

			Creates an instance of the RVJ.Core.Person dynamic .NET Type.

			*/

            Object person = personDynamicType.CreateAnInstance( "RVJ.Core.Person" );


            /*

			Gets an instance of a dynamic .NET Field.
			The search that the System.Type.GetField() instance method does, is case-sensitive.

			*/
            Type personType = person.GetType();

            FieldInfo personFieldId = personType.GetField( "_id", ( BindingFlags.NonPublic  | BindingFlags.Instance ) );
            FieldInfo personFieldName = personType.GetField( "_name", ( BindingFlags.NonPublic  | BindingFlags.Instance ) );
            FieldInfo personFieldAge = personType.GetField( "_age", ( BindingFlags.NonPublic  | BindingFlags.Instance ) );

            /*

			Shows the dynamic .NET Field values before assigning new values.

			*/


            UInt32 newId = ( UInt32 ) personFieldId.GetValue( person );
            String newName = ( String ) personFieldName.GetValue( person );
            UInt32 newAge = ( UInt32 ) personFieldAge.GetValue( person );

            if ( newName == null ) newName = String.Empty;

            Console.WriteLine( "Before new values...\nperson._id: {0}\nperson._name: {1}\nperson._age: {2}\n", newId.ToString(), newName, newAge.ToString() );

            newId = 100;
            newName = "New Name!!!";
            newAge = 25;

            personFieldId.SetValue( person, newId );
            personFieldName.SetValue( person, newName );
            personFieldAge.SetValue( person, newAge );


            newId = ( UInt32 ) personFieldId.GetValue( person );
            newName = ( String ) personFieldName.GetValue( person );
            newAge = ( UInt32 ) personFieldAge.GetValue( person );

            Console.WriteLine( "After new values assigned...\nperson._id: {0}\nperson._name: {1}\nperson._age: {2}\n", newId.ToString(), newName, newAge.ToString() );


            Program.Pause();

            /*

			Now, we are using the dynamic .NET Properties defined for each dynamic .NET Field, to do the same operations of "get" and "set" values.

			*/

            newId = ( UInt32 ) personType.InvokeMember( "Id", BindingFlags.GetProperty, null, person, null );
            newName = ( String ) personType.InvokeMember( "Name", BindingFlags.GetProperty, null, person, null );
            newAge = ( UInt32 ) personType.InvokeMember( "Age", BindingFlags.GetProperty, null, person, null );


            Console.WriteLine( "Before new values assigned...\nperson._id: {0}\nperson._name: {1}\nperson._age: {2}\n", newId.ToString(), newName, newAge.ToString() );



            newId = 200;
            newName = "New Name using a dynamic .NET Property!!!";
            newAge = 35;

            personType.InvokeMember( "Id", BindingFlags.SetProperty, null, person, new Object[] { newId } );
            personType.InvokeMember( "Name", BindingFlags.SetProperty, null, person, new Object[] { newName } );
            personType.InvokeMember( "Age", BindingFlags.SetProperty, null, person, new Object[] { newAge } );

            Console.WriteLine( "After new values...\nperson._id: {0}\nperson._name: {1}\nperson._age: {2}\n", newId.ToString(), newName, newAge.ToString() );

            Program.Pause( true );

        }
    };
};