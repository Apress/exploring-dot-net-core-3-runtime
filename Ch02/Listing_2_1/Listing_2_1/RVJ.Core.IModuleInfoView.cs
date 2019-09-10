#region Namespaces
using System;
using System.Reflection;
#endregion

namespace RVJ.Core {
	public interface IModuleInfoView : IEquatable<IModuleInfoView> {

		#region Properties
		Assembly Assembly {
			get;
		}
		String ImageFileMachineName {
			get;
		}

		String Name {
			get;
		}

		String FullyQualifiedName {
			get;
		}

		String MetadataVersion {
			get;
		}

		String PEKindName {
			get;
		}

		String VersionID {
			get;
		}

		Guid VersionIDAsGUID {
			get;
		}

		Module ManifestModule {
			get; set;
		}

		Boolean IsManifestModule {
			get;
		}
		#endregion
	};
};
