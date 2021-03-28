namespace NSync.Abstractions
{
    public static class SendConstants
    {
        public const string SendParameters = "{{\"userName\" : \"not\", \"password\":\"{0}\"}}";
        public const string TokenPath = "{0}/api/account/token";
        public const string UploadPath = "{0}/api/not";
        public const string RecordsCount = "recordCount";
    }

    public static class AppConstants
    {
        public const string SyncDateFile = "LastSyncDate.dat";
        public const string LogsParameters = "{Timestamp:dd.MM.yyyy HH:mm:ss} [{Level:u3}] {Message}{NewLine}{Exception}";
        public const string LogsPath = "logs\\log-.txt";
        public const string SettingsFile = "appsettings.json";
        public const int WaitingTime = 60000;
    }

    public static class AcquireConstants
    {
        public const string NotaryDataField = "NotData";
        public const string CreatedField = "Created";
        public const string ModifiedField = "Modified";
        public const string ChambersField = "ChamberData";
    }

    public static class PackageConstants
    {
        public const string FirstFile = "0_start.xml";
        public const string ChamberFile = "chamber{0}.xml";
    }
}
