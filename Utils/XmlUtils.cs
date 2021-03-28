using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace NSync.Utils
{
    public static class XmlUtils
    {
        /// <summary>
        /// Serializing XML from params
        /// </summary>
        /// <typeparam name="T">params type</typeparam>
        /// <param name="paramsType">params</param>
        /// <returns>Xml string</returns>
        public static string SerializeXml<T>(T paramsType)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var memoryStream = new MemoryStream())
            {
                serializer.Serialize(memoryStream, paramsType, null);
                return Encoding.UTF8.GetString(memoryStream.ToArray());
            }
        }
    }
}
