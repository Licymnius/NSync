using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NSync.Utils
{
    public static class FileUtils
    {
        /// <summary>
        /// Recurse getting files
        /// </summary>
        /// <param name="path">Files path</param>
        /// <param name="files">Check files list</param>
        /// <returns>Files list</returns>
        public static List<string> GetFiles(string path, string[] files)
        {
            var paths = new List<string>();
            try
            {
                if (files.All(file => File.Exists(Path.Combine(path, file))))
                    paths.Add(path);

                foreach (var directory in Directory.GetDirectories(path))
                    paths.AddRange(GetFiles(directory, files));
            }
            catch (UnauthorizedAccessException)
            {
            }

            return paths;
        }

        /// <summary>
        /// Creating temporary directory
        /// </summary>
        /// <returns>Temporary directory path</returns>
        public static string CreateTempFolder()
        {
            var tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(tempPath);
            return tempPath;
        }
    }
}
