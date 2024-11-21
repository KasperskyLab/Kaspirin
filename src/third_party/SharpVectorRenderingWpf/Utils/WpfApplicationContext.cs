// Copyright © 2024 AO Kaspersky Lab.

// This file has been modified by AO Kaspersky Lab in 1/11/2023.
// Scope of modification:
//   - Code adaptation to project requirements.

using System;
using System.IO;
using System.Security;
using System.Security.Permissions;

namespace SharpVectors.Renderers.Utils
{
    public sealed class WpfApplicationContext
    {
        public static DirectoryInfo ExecutableDirectory
        {
            get
            {
                try
                {
                    FileIOPermission f = new FileIOPermission(PermissionState.None);
                    f.AllLocalFiles = FileIOPermissionAccess.Read;

                    f.Assert();

                    return new DirectoryInfo(Path.GetDirectoryName(
                        System.Reflection.Assembly.GetExecutingAssembly().Location) ?? Directory.GetCurrentDirectory());
                }
                catch(SecurityException)
                {
                    return new DirectoryInfo(Directory.GetCurrentDirectory());
                }
            }
        }

        public static DirectoryInfo DocumentDirectory
        {
            get
            {
                return new DirectoryInfo(Directory.GetCurrentDirectory());
            }
        }

        public static Uri DocumentDirectoryUri
        {
            get
            {
                string sUri = DocumentDirectory.FullName + "/";
                sUri = "file://" + sUri.Replace("\\", "/");
                return new Uri(sUri);
            }
        }
    }
}
