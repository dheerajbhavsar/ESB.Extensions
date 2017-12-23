using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace ESB.Extensions.Resolution
{
    [Serializable]
    public class ResolutionDictionary : Dictionary<string, object> //, ISerializable
    {
        public ResolutionDictionary()
        {
        }

        protected ResolutionDictionary(SerializationInfo si, StreamingContext sc)
            : base(si, sc)
        {
        }

        //public ResolutionDictionary(SerializationInfo si, StreamingContext sc)
        //{
        //    string keys = si.GetString("{6DA19447-C7DA-45d0-850D-21A9AC26F02A}");
        //    string[] keyArray = keys.Split('|');
        //    foreach (string key in keyArray)
        //    {
        //        if (!string.IsNullOrEmpty(key))
        //        {
        //            object val = si.GetValue(key, typeof(object));
        //            SetValue(key, val);
        //        }
        //    }
        //}

        //private static char[] _tc = new char[] { '|' };
        //void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    foreach (string key in this.Keys)
        //    {
        //        info.AddValue(key, this[key]);
        //        sb.Append(key);
        //        sb.Append('|');
        //    }
        //    info.AddValue("{6DA19447-C7DA-45d0-850D-21A9AC26F02A}", sb.ToString().TrimEnd(_tc));
        //}

        public void SetValue(string key, object value)
        {
            if (!this.ContainsKey(key))
            {
                base.Add(key, value);
            }
            else
            {
                this[key] = value;
            }
        }

        public object GetValue(string key)
        {
            if (base.ContainsKey(key))
            {
                return base[key];
            }
            return null;
        }

        public string GetString(string key)
        {
            return GetValue(key) as string;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("ESB.Extensions.Resolution.ResolutionDictionary; ");
            foreach (string key in this.Keys)
            {
                sb.Append(key);
                sb.Append(": ");
                object o = this[key];
                sb.Append(o ?? "<null>");
            }
            return sb.ToString();
        }
    }
}
