using System;
using System.Xml.Serialization;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace ESB.Extensions.PipelineConponents.ContextAdder
{
    [XmlRoot(ElementName = "ContextPropertyInfoCollection", Namespace = "urn:ESB/Extensions/PipelineConponents/ContextAdder")]
    [XmlTypeAttribute(Namespace = "urn:ESB/Extensions/PipelineConponents/ContextAdder")]
    public class ContextPropertyInfoCollection : Collection<ContextPropertyInfo>
    {
    }

    [XmlRoot(ElementName = "ContextPropertyInfo", Namespace = "urn:ESB/Extensions/PipelineConponents/ContextAdder")]
    [XmlTypeAttribute(Namespace = "urn:ESB/Extensions/PipelineConponents/ContextAdder")]
    public class ContextPropertyInfo
    {
        private string _propertyName = string.Empty;
        public string PropertyName
        {
            get { return _propertyName; }
            set { _propertyName = value; }
        }

        private string _propertyNamespace = string.Empty;
        public string PropertyNamespace
        {
            get { return _propertyNamespace; }
            set { _propertyNamespace = value; }
        }

        private string _propertyValue = string.Empty;
        public string PropertyValue
        {
            get { return _propertyValue; }
            set { _propertyValue = value; }
        }

        private bool _promoted = false;
        public bool Promoted
        {
            get { return _promoted; }
            set { _promoted = value; }
        }
    }
}
