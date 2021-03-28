using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using NSync.Abstractions;
using NSync.Entities;
using NSync.Interfaces;
using NSync.Utils;
using Serilog;

namespace NSync.Transients
{
    /// <summary>
    /// Writing and packing notaries
    /// </summary>
    public class Archiver : IArchiver
    {
        /// <summary>
        /// Writing and packing notaries information
        /// </summary>
        /// <param name="notaries">Notaries list</param>
        /// <param name="uploadInfo">Uploading information</param>
        /// <param name="chambers">Notaries chambers list</param>
        /// <returns>Package file name</returns>
        public string PackNotaries(IEnumerable<Notary> notaries, UploadInfo uploadInfo, List<string> chambers)
        {
            var tempFolder = FileUtils.CreateTempFolder();
            try
            {
                Log.Information("Writing common information");
                File.WriteAllText(Path.Combine(tempFolder, PackageConstants.FirstFile), XmlUtils.SerializeXml(uploadInfo));

                Log.Information("Writing chambers information");
                for (var index = 0; index < chambers.Count; index++)
                    File.WriteAllText(Path.Combine(tempFolder, string.Format(PackageConstants.ChamberFile, index)), chambers[index].Replace("encoding=\"windows-1251\"", "encoding=\"utf-8\""));

                Log.Information("Writing notaries information");
                foreach (var notary in notaries)
                    File.WriteAllText(Path.Combine(tempFolder, notary.NotData.NotID + ".xml"), XmlUtils.SerializeXml(notary));

                Log.Information("Packing information");
                var archiveName = Path.GetTempFileName() + ".zip";
                ZipFile.CreateFromDirectory(tempFolder, Path.Combine(Path.GetTempPath(), archiveName));
                return archiveName;
            }
            finally
            {
                if (Directory.Exists(tempFolder))
                    Directory.Delete(tempFolder, true);
            }
        }
    }
}
