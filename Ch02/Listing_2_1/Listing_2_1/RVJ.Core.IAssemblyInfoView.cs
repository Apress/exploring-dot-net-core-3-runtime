#region Namespaces
using System;
using System.Reflection;
#endregion

namespace RVJ.Core {
    public interface IAssemblyInfoView {


        #region Methods
        void DoAsync( TypeInfo typeInfo );
        #endregion

        #region Properties
        String FullName {
            get;
        }


        String EntryPointMethodName {
            get;
        }

        String Location {
            get;
        }

        Boolean IsPublishedOnGAC {
            get;
        }

        Boolean IsFullyTrusted {
            get;
        }

        Boolean IsReflectionOnly {
            get;
        }

        String CodeBase {
            get;
        }

        TypeInfo[] DefinedTypes {
            get;
        }
        Boolean IsDynamic {
            get;
        }

        String ImageRuntimeVersion {
            get;
        }

        String ImageFileMachineName {
            get;
        }
        Boolean IsStatic {
            get;
        }

        Boolean IsMultiModule {
            get;
        }

        String Name {
            get;
        }

        String CultureName {
            get;
        }

        String VersionCompatibility {
            get;
        }

        String VersionMajorNumber {
            get;
        }

        String VersionMinorNumber {
            get;
        }

        String VersionBuildNumber {
            get;
        }

        String VersionRevisionNumber {
            get;
        }

        String VersionMajorRevisionNumber {
            get;
        }

        String VersionMinorRevisionNumber {
            get;
        }

        String PublicKeyTokenValue {
            get;
        }

        String ManifestModuleName {
            get;
        }

        String MetadataStreamVersion {
            get;
        }

        String MetadataToken {
            get;
        }

        String PEKindName {
            get;
        }
        #endregion

    };
};
