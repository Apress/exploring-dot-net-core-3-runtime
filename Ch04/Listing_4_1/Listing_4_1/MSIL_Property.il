/*
Author: Roger Villela
E-mail: Roger@OpenMind.OnMicrosoft.com

Instructions

For compile this, open the Developer Command Prompt of Microsoft Visual Studio or .NET Framework SDK, write the following command for generate a 32-bit executable:

ilasm  MSIL_Poperty.il

OR for generate a 64-bit executable:

ilasm /X64 MSIL_Poperty.il


*/
	.assembly extern mscorlib{}

	.assembly RVJ.Core {}

.class public auto ansi beforefieldinit RVJ.Core.Example00 extends System.Object {

	/* Private fields */
	.field private string _name


	/* Constructors */
	.method public hidebysig specialname rtspecialname instance void  .ctor() cil managed {
  
		.maxstack 4

	   ldarg.0
	   call       instance void [mscorlib]System.Object::.ctor()
	   ret

	} /* end of constructor RVJ.Core.Example00::.ctor */

	/* 
	Public properties 


	The methods marked .get, .set and .other property's attribute are not called automatically. 

	Some tool, such a compiler, should emit MSIL instructions to do these callings of methods or these callings of methods should 
	be made by MSIL instructions emitted by a custom tool.

	*/
	.property instance string Name() {
		.get instance string RVJ.Core.Example00::get_Name()
		.set instance void RVJ.Core.Example00::set_Name(string)
		.other instance void RVJ.Core.Example00::VerifyName(string)
		.other instance void RVJ.Core.Example00::AnotherVerifyName(string)
	} /* end of property Example00::Name */


	/*



	*/

	.method public hidebysig specialname instance void VerifyName( string name ) cil managed {

		.maxstack 8
		.locals init ( int32 length )

		ldstr "VerifyName() method called. Length of name: {0}\n\n"
		ldarg.1  /* Load the name argument value. */
		callvirt instance int32 [mscorlib]System.String::get_Length()
		stloc.0  /* Stores the value on length  local variable. */
		ldloca length
	    callvirt instance string valuetype [mscorlib]System.Int32::ToString()
		call void [mscorlib]System.Console::WriteLine( string, object )

		ret
	}

	.method public hidebysig specialname instance void AnotherVerifyName(string name) cil managed {

		.maxstack 4

		ldarg.1 /* Load the name argument value. */
		callvirt instance int32 [mscorlib] System.String::get_Length()
		pop
		callvirt instance string valuetype [mscorlib]System.Int32::ToString()
		ldstr "AnotherVerifyName() method called. Length of value: {0}"
		call void [mscorlib]System.Console::WriteLine( string, object )

		ret
	}

	/* Getter method for the property Name. */
	.method public hidebysig specialname instance string get_Name() cil managed {
  
	.maxstack 4

	.locals init (string currentName)

    ldarg.0
    ldfld      string RVJ.Core.Example00::_name
    stloc.0
	ldloc.0
   
   ret
} /* end of method Example00::get_Name */

/* Setter method for the property Name. */
.method public hidebysig specialname instance void  set_Name(string 'value') cil managed {

	.maxstack 4

    ldarg.0 /* Load this pointer. */
    ldarg.1 /* Load the 'value' argument value */
    stfld      string RVJ.Core.Example00::_name
  
	ret
} /* end of method Example00::set_Name */


	/* Entry-point method. */
	.method public static  hidebysig void Main() cil managed {

		.maxstack 8
		.entrypoint

		.locals init ( class RVJ.Core.Example00 myInstance )
	
		newobj instance void RVJ.Core.Example00::.ctor()
		stloc.0 /* myInstance */
		ldloc.0 /* myInstance */
		call void [mscorlib]System.Console::Clear()
		ldstr "New Name"
		callvirt instance void RVJ.Core.Example00::VerifyName( string )
		ldloc.0 /* myInstance */
		ldstr "New Name"
		callvirt instance void RVJ.Core.Example00::set_Name( string )
		ldstr "Name: {0}"
		ldloc.0
		callvirt instance string RVJ.Core.Example00::get_Name()
		call void [mscorlib]System.Console::WriteLine( string, object )


		ldstr "\n\nPress <ENTER> to finish..."
		call void [mscorlib]System.Console::WriteLine(string)
		call string [mscorlib]System.Console::ReadLine()
		pop

		ret
}


} /* end of class RVJ.Core.Example00 */


