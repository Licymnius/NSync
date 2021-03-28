using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using NSync.Abstractions;
using NSync.Configuration;
using NSync.Interfaces;
using Serilog;

namespace NSync.Services
{
    /// <summary>
    /// Time watcher to send notaries
    /// </summary>
    public class NotWatcherService : BackgroundService
    {
        private readonly INotProcessor _notProcessor;
        private readonly NSyncConfiguration _nSyncConfiguration;
        private DateTime _lastSyncDateTime;
        private readonly string _dateFile;

        public NotWatcherService(
            IOptions<NSyncConfiguration> options,
            INotProcessor notProcessor)
        {
            _notProcessor = notProcessor;

            var filePath = Process.GetCurrentProcess().MainModule?.FileName;
            _dateFile = (filePath == null ? null : Path.GetDirectoryName(filePath) + "\\") + AppConstants.SyncDateFile;
            _nSyncConfiguration = options.Value;
            
            if (File.Exists(_dateFile))
                DateTime.TryParse(File.ReadAllText(_dateFile), out _lastSyncDateTime);
        }
        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Log.Information("Worker start running");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    if ((DateTime.Now - _lastSyncDateTime).TotalHours > _nSyncConfiguration.SyncTimePeriod)
                    {
                        Log.Information("Starting sync process");
                        await _notProcessor.ProcessNotaries();
                        _lastSyncDateTime = DateTime.Now;

                        //Writing last synchronization info
                        await File.WriteAllTextAsync(_dateFile, _lastSyncDateTime.ToString("G"), stoppingToken);
                    }
                }
                catch (Exception exc)
                {
                    Log.Error(exc, "Notaries Acquiring Error");
                }
                
                await Task.Delay(AppConstants.WaitingTime, stoppingToken);
                stoppingToken.ThrowIfCancellationRequested();
            }
        }
    }
}
