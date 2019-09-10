#region Namespaces
using System;
using System.Reflection;
using RVJ.Core;
#endregion


namespace Listing_3_1 {
	public class Program {
		static void Main() {


			System.Reflection.Emit.pro












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


			personDynamicType.DefineDynamicField( "_id", typeof( System.UInt32 ), FieldFlags.Private );
			personDynamicType.DefineDynamicField( "_name", typeof( System.String ), FieldFlags.Private );
			personDynamicType.DefineDynamicField( "_age", typeof( System.UInt32 ), FieldFlags.Private );

			/*
			
			Creates an instance of the RVJ.Core.Person dynamic .NET Type.
			
			*/

			Object person = personDynamicType.CreateAnInstance( "RVJ.Core.Person" );


			/*
			
			Gets an instance of a dynamic .NET Field. 
			The search that the System.Type.GetGetField() instance method does, is case-sensitive.

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

			Console.ReadLine();
			Console.WriteLine( "Press <ENTER> to finish..." );

		}
	};
};