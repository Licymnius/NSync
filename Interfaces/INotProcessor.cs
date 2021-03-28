using System.Threading.Tasks;

namespace NSync.Interfaces
{
    /// <summary>
    /// Common notaries processing
    /// </summary>
    public interface INotProcessor
    {
        /// <summary>
        /// Process acquiring and upload data
        /// </summary>
        public Task ProcessNotaries();
    }
}
