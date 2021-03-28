using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using NSync.Abstractions;
using NSync.Configuration;
using NSync.Interfaces;
using Serilog;

namespace NSync.Transients
{
    public class NotSender : INotSender
    {
        readonly NSyncConfiguration _nSyncConfiguration;

        public NotSender(IOptions<NSyncConfiguration> nSyncConfiguration)
        {
            _nSyncConfiguration = nSyncConfiguration.Value;
        }

        /// <summary>
        /// Uploading retrieved information
        /// </summary>
        /// <param name="packageName">Uploading package name</param>
        /// <param name="recordsCount">Uploading records count</param>
        public async Task SendAsync(string packageName, int recordsCount)
        {
            Log.Information("Sending package {time}", DateTimeOffset.Now);
            try
            {
                using (var httpClientHandler = new HttpClientHandler())
                {
                    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
                    using (var client = new HttpClient(httpClientHandler))
                    {
                        var loginPair = string.Format(SendConstants.SendParameters, _nSyncConfiguration.WebServicePassword);
                        Console.WriteLine(loginPair);
                        var rest = await client.PostAsync( string.Format(SendConstants.TokenPath, _nSyncConfiguration.WebServiceUrl),
                            new StringContent(loginPair, Encoding.UTF8, "application/json"));
                        var token = await rest.Content.ReadAsStringAsync();
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                        var name = await client.GetStringAsync(string.Format(SendConstants.UploadPath, _nSyncConfiguration.WebServiceUrl));
                        using (var form = new MultipartFormDataContent())
                        using (var fileContent = new ByteArrayContent(await File.ReadAllBytesAsync(packageName)))
                        {
                            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
                            form.Add(fileContent, "zipFile", Path.GetFileName(packageName));
                            form.Add(new StringContent(recordsCount.ToString()), SendConstants.RecordsCount);
                            var re = await client.PostAsync(string.Format(SendConstants.UploadPath, _nSyncConfiguration.WebServiceUrl), form);
                            Log.Information($"Successfully sent to {name}");
                        }
                    }
                }
            }
            finally
            {
                if (File.Exists(packageName))
                    File.Delete(packageName);
            }
        }
    }
}
