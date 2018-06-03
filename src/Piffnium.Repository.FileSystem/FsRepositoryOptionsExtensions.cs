using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Piffnium.Repository.FileSystem
{
    public static class FsRepositoryOptionsExtensions
    {
        public static string GetDirectoryNameWithRoot(this FsRepositoryOptions self, string directoryName)
        {
            var rootDir = self.RootDirectory;
            if (string.IsNullOrEmpty(rootDir))
            {
                return directoryName;
            }
            return Path.Combine(rootDir, directoryName);
        }

        public static string GetDirectoryNameWithRoot(this FsRepositoryOptions self, long numberdDirectoryName)
        {
            return self.GetDirectoryNameWithRoot(numberdDirectoryName.ToString());
        }
    }
}
