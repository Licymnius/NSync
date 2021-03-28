using System.Collections.Generic;
using NSync.Entities;

namespace NSync.Interfaces
{
    /// <summary>
    /// Writing and packing notaries
    /// </summary>
    public interface IArchiver
    {
        /// <summary>
        /// Writing and packing notaries information
        /// </summary>
        /// <param name="notaries">Notaries list</param>
        /// <param name="uploadInfo">Uploading information</param>
        /// <param name="chambers">Notaries chambers list</param>
        /// <returns>Package file name</returns>
        string PackNotaries(IEnumerable<Notary> notaries, UploadInfo uploadInfo, List<string> chambers);
    }
}
