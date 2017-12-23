using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ESB.Extensions.Resolutions
{
    [Serializable]
    public class Serializable<T>
        where T : new()
    {
        internal static T CreateInstanceFromXml(string xml)
        {
            using (StringReader sr = new StringReader(xml))
            {
                using (XmlReader xr = XmlReader.Create(sr))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(T));
                    return (T)xs.Deserialize(xr);
                }
            }
        }

        public string CreateXmlFromInstance()
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            using (StringWriter sw = new StringWriter())
            {
                xs.Serialize(sw, this);
                return sw.ToString();
            }
        }

        public Stream CreateStreamFromInstance()
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            MemoryStream ms = new MemoryStream();
            xs.Serialize(ms, this);
            ms.Seek(0, SeekOrigin.Begin);
            return ms;
        }
    }
}
