using System;
using System.Linq;
using System.Threading.Tasks;
using NSync.Interfaces;
using Serilog;

namespace NSync.Transients
{
    /// <summary>
    /// Common notaries processing
    /// </summary>
    public class NotProcessor : INotProcessor
    {
        private readonly INotSender _notSender;
        private readonly INotAcquirer _notAcquirer;
        private readonly IArchiver _archiver;

        public NotProcessor(
            INotAcquirer notAcquirer,
            INotSender notSender,
            IArchiver archiver)
        {
            _notSender = notSender;
            _notAcquirer = notAcquirer;
            _archiver = archiver;
        }

        /// <summary>
        /// Process acquiring and upload data
        /// </summary>
        public async Task ProcessNotaries()
        {
            try
            {
                var notaries = _notAcquirer.RetrieveNotaries();
                if (!notaries.Any())
                    throw new Exception("Notaries data not found");

                var chambers = _notAcquirer.RetrieveChambers();
                var uploadInfo = _notAcquirer.GetUploadInfo();
                await _notSender.SendAsync(_archiver.PackNotaries(notaries, uploadInfo, chambers), _notAcquirer.RecordsCount);
            }
            catch (Exception exc)
            {
                Log.Error(exc, "Error while processing notaries");
            }
        }
    }
}
