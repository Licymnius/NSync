using System;
using System.Xml.Serialization;

namespace NSync.Entities
{
    [XmlRoot("NData", IsNullable = false)]
    public class Notary
    {
        public DateTime? Created { get; set; }

        public DateTime Modified { get; set; }

        public NotData NotData { get; set; }
    }
}