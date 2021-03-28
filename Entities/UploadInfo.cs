namespace NSync.Entities
{
    public class UploadInfo
    {
        /// <summary>
        /// Версия программы-посредника;
        /// </summary>
        public string SyncVersion { get; set; }

        /// <summary>
        /// версия АПИ;
        /// </summary>
        public string ApiVersion { get; set; }
        
        /// <summary>
        /// версия ИСН;
        /// </summary>
        public string NotVersion { get; set; }

        /// <summary>
        /// Имя компьютера
        /// </summary>
        public string PcName { get; set; }

    }
}
