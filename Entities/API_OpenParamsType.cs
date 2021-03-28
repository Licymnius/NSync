using System;
using System.Xml.Serialization;

namespace NSync.Entities
{
    [Serializable]
    [XmlRoot("API_OpenParams", IsNullable = false)]
    public class API_OpenParamsType
    {
        public API_OpenParamsTypeFilter Filter { get; set; }
    }

    [Serializable]
    public class API_OpenParamsTypeFilter
    {
        public string ChamberID { get; set; }
        public string Real { get; set; }
    }
}