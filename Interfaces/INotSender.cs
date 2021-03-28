using System.Threading.Tasks;

namespace NSync.Interfaces
{
    public interface INotSender
    {
        /// <summary>
        /// Uploading retrieved information
        /// </summary>
        /// <param name="packageName">Uploading package name</param>
        /// <param name="recordsCount">Uploading records count</param>
        Task SendAsync(string packageName, int recordsCount);
    }
}
