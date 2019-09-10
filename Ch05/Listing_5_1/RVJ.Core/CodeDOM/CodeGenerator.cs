#region Namespaces
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;
#endregion

namespace RVJ.Core.CodeDOM {
    public class CodeGenerator {

        #region Private fields
        private Boolean _hasGenerated; /* Used to check if the code was generated. */
        private CodeDomProvider _provider;
        private CodeCompileUnit _compileUnit;
        #endregion

        #region Constructors
        public CodeGenerator() : base() {

            this._hasGenerated = new Boolean();

            return;
        }
        #endregion

        #region Properties
        public ProgrammingLanguages ProgrammingLanguage { get; set; }
        public AssemblyInformation AssemblyInformation { get; set; }

        public String RootNamespace { get; set; }

        public String SourceFileName { get; set; }

        #endregion

        #region Public Behaviors
        /* Generates the source code. */
        public Boolean Generate() {

            return this.__CodeGenerator();

        }

        public Boolean Save() {

            return this.__Save( ref this._compileUnit, ref this._provider );

        }

        #endregion

        #region Private Behaviors

        private Boolean __CreateProvider( ProgrammingLanguages programmingLanguage, ref CodeDomProvider provider ) {

            Boolean hasTheProvider = new Boolean();
            String language = programmingLanguage.ToString();

            switch ( programmingLanguage ) {

                case ProgrammingLanguages.CSharp:
                    hasTheProvider = CodeDomProvider.IsDefinedLanguage( language );
                    break;
                case ProgrammingLanguages.VisualBasic:
                    hasTheProvider = CodeDomProvider.IsDefinedLanguage( language );
                    break;
                default: /*  C# Programming Language. */
                    language = "C#";
                    hasTheProvider = !hasTheProvider;
                    break;

            };

            provider = CodeDomProvider.CreateProvider( language );

            return hasTheProvider;
        }
        private Boolean __CodeGenerator() {

            ProgrammingLanguages pl = this.ProgrammingLanguage;
            AssemblyInformation ai = this.AssemblyInformation;
            String assemblyName = ai.Name;
            String rootNamespace = this.RootNamespace;
            TypeInformation[] types = ai.Types;
            Int32 index = new Int32();

            this.__CreateProvider( pl, ref this._provider );

            CodeCompileUnit compileUnit = new CodeCompileUnit();

            /* Declare a new namespace. */
            CodeNamespace rn = new CodeNamespace( rootNamespace );

            CodeNamespaceImport[] cnic = new CodeNamespaceImport[ ai.Imports.Length ];
            for ( ; index <  ai.Imports.Length; index++ ) cnic[ index ] = new CodeNamespaceImport( ai.Imports[ index ] );

            rn.Imports.AddRange( cnic );

            /* Create  new types. */
            CodeTypeDeclaration[] ctd = new CodeTypeDeclaration[ types.Length ];

            /* Declare new type. */
            for ( index = new Int32(); index < types.Length; index++ ) {

                FieldInformation[] _fields = types[ index ].Fields;

                ctd[ index ] = new CodeTypeDeclaration( types[ index ].Name );

                /* For each code type, create the field members. */
                for ( Int32 fIndex = new Int32(); fIndex < _fields.Length; fIndex++ ) {

                    ctd[ index ].Members.Add( new CodeMemberField( _fields[ fIndex ].TypeReference, _fields[ index ].Name ) );
                    ctd[ index ].Members[ index ].Attributes = ( MemberAttributes ) _fields[ fIndex ].Attributes;

                };
            };

            rn.Types.AddRange( ctd );

            /* Adds the new namespace to the compile unit. */
            compileUnit.Namespaces.Add( rn );

            this._compileUnit = compileUnit;



            return this._hasGenerated = true;
        }

        private Boolean __Save( ref CodeCompileUnit unit, ref CodeDomProvider provider ) {

            Boolean saved = new Boolean();

            if ( this._hasGenerated ) {

                using ( IndentedTextWriter itw = new IndentedTextWriter( new StreamWriter( this.SourceFileName ) ) ) {

                    provider.GenerateCodeFromCompileUnit( unit, itw, new CodeGeneratorOptions() );

                    itw.Close();
                }

                saved = !saved;

            }

            return saved;

        }
        #endregion

    }
};