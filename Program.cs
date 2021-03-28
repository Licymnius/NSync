using System.Diagnostics;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NSync.Abstractions;
using NSync.Configuration;
using NSync.Interfaces;
using NSync.Services;
using NSync.Transients;
using Serilog;
using Serilog.Events;

namespace NSync
{
    /// <summary>
    /// sc.exe create NSync binpath= c:\ProgramData\NSync\NSync.exe  type= interact type= own start= auto 
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File(AppConstants.LogsPath, rollingInterval: RollingInterval.Year,
                    outputTemplate: AppConstants.LogsParameters, restrictedToMinimumLevel: LogEventLevel.Error)
                .CreateLogger();

            CreateHostBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var filePath = Process.GetCurrentProcess().MainModule?.FileName;
                    
                    if (filePath != null)
                        config.SetBasePath(Path.GetDirectoryName(filePath) + "\\");

                    config.AddJsonFile(AppConstants.SettingsFile, optional: false, reloadOnChange: true);
                })
                .ConfigureLogging((context, logging) =>
                {
                    logging.AddConsole();
                    logging.SetMinimumLevel(LogLevel.Trace);

                })
                .Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args)
            
            .UseWindowsService().ConfigureServices((hostContext, services) =>
        {
            services.Configure<NSyncConfiguration>(hostContext.Configuration);
            services.AddTransient<INotProcessor, NotProcessor>();
            services.AddTransient<INotSender, NotSender>();
            services.AddTransient<IArchiver, Archiver>();
            services.AddTransient<INotAcquirer, NotAcquirer>();
            services.AddHostedService<NotWatcherService>();
        }).UseWindowsService();
    }
}
