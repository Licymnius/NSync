using System;
using System.Xml.Serialization;

namespace NSync.Entities
{
    /// <summary>
    /// Данные о переданном архиве
    /// </summary>
    [Serializable]
    public class Archive
    {
        /// <summary>
        /// ID нотариуса, чей архив передан
        /// </summary>
        [XmlElement("ArcNotaryID")]
        public string ArcNotaryId { get; set; }

        /// <summary>
        /// Дата передачи архива
        /// </summary>
        public DateTime DateOn { get; set; }

        /// <summary>
        /// Основание для передачи архива
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// Примечание
        /// </summary>
        public string Note { get; set; }
    }
}
