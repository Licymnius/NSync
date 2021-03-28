using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;
using Microsoft.Extensions.Options;
using NSync.Abstractions;
using NSync.Configuration;
using NSync.Entities;
using NSync.Interfaces;
using NotApi2;
using Serilog;

namespace NSync.Transients
{
    /// <summary>
    /// Acquiring notaries using COM object NotApi2
    /// </summary>
    public class NotAcquirer : INotAcquirer
    {
        private clsAPI _api;
        
        private int _recordsCount;

        /// <summary>
        /// Uploading records count
        /// </summary>
        public int RecordsCount => _recordsCount;

        public NotAcquirer(IOptions<NSyncConfiguration> options)
        {
            InitNot(options.Value.NotPath);
        }

        private void InitNot(string notPath)
        {
            _api = new clsAPI();
            if (!_api.Initialize(notPath))
                throw new Exception("Not Initialization error");
        }

        /// <summary>
        /// Retrieving notaries Chambers
        /// </summary>
        /// <returns>Chambers List</returns>
        public List<string> RetrieveChambers()
        {
            var vrsChambers = new vrsChambersClass();
            vrsChambers.OpenView();
            var chambersXmls = new List<string>(vrsChambers.RecordCount());

            try
            {
                while (!vrsChambers.IsEOF())
                {
                    chambersXmls.Add((string)vrsChambers.GetField(AcquireConstants.ChambersField));
                    vrsChambers.MoveNext();
                }

                return chambersXmls;
            }
            finally
            {
                vrsChambers.CloseView();
            }
        }

        /// <summary>
        /// Retrieving notaries
        /// </summary>
        /// <returns>Notaries list</returns>
        public List<Notary> RetrieveNotaries()
        {
            Log.Information("Starting Acquiring Notaries from not");
            var vrsNotaries = new vrsNotaries();
            vrsNotaries.OpenView( /*SerializeXml(new API_OpenParamsType { Filter = new API_OpenParamsTypeFilter { ChamberID = "022", Real = "1" } })*/);
            try
            {
                _recordsCount = vrsNotaries.RecordCount();
                var notaries = new List<Notary>(_recordsCount);
                var notaryIndex = 1;
                var progressPercentage = 0;
                while (!vrsNotaries.IsEOF())
                {
                    var notary = new Notary();
                    var created = vrsNotaries.GetField(AcquireConstants.CreatedField);
                    notary.Created = (DateTime?)(created.Equals(DBNull.Value) ? null : created);
                    var modified = vrsNotaries.GetField(AcquireConstants.ModifiedField);
                    notary.Modified = (DateTime)(modified.Equals(DBNull.Value) ? null : modified);

                    TextReader reader = new StringReader((string)vrsNotaries.GetField(AcquireConstants.NotaryDataField));
                    try
                    {
                        var serializer = new XmlSerializer(typeof(NotData));
                        notary.NotData = (NotData)serializer.Deserialize(reader);
                    }
                    finally
                    {
                        reader.Close();
                    }

                    notaries.Add(notary);
                    vrsNotaries.MoveNext();
                    var percentage = (int)Math.Round(notaryIndex++ / (float)_recordsCount * 100);
                    if (percentage == progressPercentage)
                        continue;

                    Console.WriteLine("Notaries " + percentage + "%");
                    progressPercentage = percentage;
                }

                return notaries;
            }
            finally
            {
                vrsNotaries.CloseView();
            }
        }

        /// <summary>
        /// Getting uploading info
        /// </summary>
        /// <returns>Info on uploading</returns>
        public UploadInfo GetUploadInfo()
        {
            return new UploadInfo
            {
                ApiVersion = _api.Version,
                SyncVersion = Assembly.GetExecutingAssembly().GetName().Version?.ToString(),
                PcName = Environment.MachineName
            };
        }

        public void Dispose()
        {
            _api = null;
        }
    }
}
