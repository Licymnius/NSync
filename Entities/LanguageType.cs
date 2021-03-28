using System;
using System.Xml.Serialization;

namespace NSync.Entities
{
    /// <summary>
    /// Язык перевода
    /// </summary>
    [Serializable]
    [XmlType("Language")]
    public class LanguageType
    {
        /// <summary>
        /// Язык перевода
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Примечание
        /// </summary>
        public string Note { get; set; }
    }
}
