using System;
using System.Collections.Generic;
using NSync.Entities;

namespace NSync.Interfaces
{
    /// <summary>
    /// Acquiring notaries using COM object NotApi2
    /// </summary>
    public interface INotAcquirer : IDisposable
    {
        /// <summary>
        /// Uploading records count
        /// </summary>
        int RecordsCount { get; }

        /// <summary>
        /// Retrieving notaries
        /// </summary>
        /// <returns>Notaries list</returns>
        List<Notary> RetrieveNotaries();

        /// <summary>
        /// Retrieving notaries Chambers
        /// </summary>
        /// <returns>Chambers List</returns>
        List<string> RetrieveChambers();

        /// <summary>
        /// Getting uploading info
        /// </summary>
        /// <returns>Info on uploading</returns>
        UploadInfo GetUploadInfo();
    }
}
