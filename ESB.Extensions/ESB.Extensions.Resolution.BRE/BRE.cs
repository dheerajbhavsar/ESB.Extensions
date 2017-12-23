using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.ComponentModel;
using System.CodeDom.Compiler;
using System.Diagnostics;

namespace ESB.Extensions.Resolution.BRE
{
    [Serializable, XmlType(AnonymousType = true), DesignerCategory("code"), XmlRoot(Namespace = "http://ESB.Extensions.Resolution.BRE.BRE", IsNullable = false), GeneratedCode("xsd", "2.0.50727.42"), DebuggerStepThrough]
    public class BRE
    {
        // Fields
        private string policyField;
        private bool recognizeMessageFormatField;
        private bool recognizeMessageFormatFieldSpecified;
        private bool useMsgField;
        private bool useMsgFieldSpecified;
        private string versionField;
        private string propertyTypeNamesStringField;
        private bool propertyTypeNamesStringFieldSpecified;
        private Type[] propertyTypes;

        // Properties
        [XmlAttribute]
        public string policy
        {
            get { return this.policyField; }
            set { this.policyField = value; }
        }

        [XmlAttribute]
        public bool recognizeMessageFormat
        {
            get { return this.recognizeMessageFormatField; }
            set { this.recognizeMessageFormatField = value; }
        }

        [XmlIgnore]
        public bool recognizeMessageFormatSpecified
        {
            get { return this.recognizeMessageFormatFieldSpecified; }
            set { this.recognizeMessageFormatFieldSpecified = value; }
        }

        [XmlAttribute]
        public bool useMsg
        {
            get { return this.useMsgField; }
            set { this.useMsgField = value; }
        }

        [XmlIgnore]
        public bool useMsgSpecified
        {
            get { return this.useMsgFieldSpecified; }
            set { this.useMsgFieldSpecified = value; }
        }

        [XmlAttribute]
        public string version
        {
            get { return this.versionField; }
            set { this.versionField = value; }
        }

        [XmlAttribute]
        public string PropertyTypeNamesString
        {
            get { return this.propertyTypeNamesStringField; }
            set { this.propertyTypeNamesStringField = value; }
        }

        [XmlIgnore]
        public bool PropertyTypeNamesStringSpecified
        {
            get { return this.propertyTypeNamesStringFieldSpecified; }
            set { this.propertyTypeNamesStringFieldSpecified = value; }
        }

        [XmlIgnore]
        public Type[] PropertyTypes
        {
            get
            {
                if ((this.PropertyTypeNamesStringSpecified) && (!string.IsNullOrEmpty(this.PropertyTypeNamesString)) &&
                    ((null == this.propertyTypes) || (this.propertyTypes.Length <= 0)))
                {
                    string[] typeNames = this.PropertyTypeNamesString.Split('|');
                    List<Type> types = new List<Type>(typeNames.Length);
                    foreach (string typeName in typeNames)
                    {
                        types.Add(Type.GetType(typeName));
                    }
                    this.propertyTypes = types.ToArray();
                }
                return this.propertyTypes;
            }
        }
    }
}
