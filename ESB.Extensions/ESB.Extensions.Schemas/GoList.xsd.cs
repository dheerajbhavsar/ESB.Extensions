namespace ESB.Extensions.Schemas {
    using Microsoft.XLANGs.BaseTypes;
    
    
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.BizTalk.Schema.Compiler", "3.0.1.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [SchemaType(SchemaTypeEnum.Document)]
    [Schema(@"http://ESB.Extensions.Schemas.GoList",@"GoList")]
    [BodyXPath(@"/*[local-name()='GoList' and namespace-uri()='http://ESB.Extensions.Schemas.GoList']")]
    [System.SerializableAttribute()]
    [SchemaRoots(new string[] {@"GoList"})]
    [Microsoft.XLANGs.BaseTypes.SchemaReference(@"ESB.Extensions.Schemas.Go", typeof(global::ESB.Extensions.Schemas.Go))]
    public sealed class GoList : Microsoft.XLANGs.BaseTypes.SchemaBase {
        
        [System.NonSerializedAttribute()]
        private static object _rawSchema;
        
        [System.NonSerializedAttribute()]
        private const string _strSchema = @"<?xml version=""1.0"" encoding=""utf-16""?>
<xs:schema xmlns=""http://ESB.Extensions.Schemas.GoList"" xmlns:b=""http://schemas.microsoft.com/BizTalk/2003"" xmlns:go=""http://ESB.Extensions.Schemas.Go"" targetNamespace=""http://ESB.Extensions.Schemas.GoList"" xmlns:xs=""http://www.w3.org/2001/XMLSchema"">
  <xs:import schemaLocation=""ESB.Extensions.Schemas.Go"" namespace=""http://ESB.Extensions.Schemas.Go"" />
  <xs:annotation>
    <xs:appinfo>
      <b:schemaInfo is_envelope=""yes"" />
      <b:references>
        <b:reference targetNamespace=""http://ESB.Extensions.Schemas.Go"" />
      </b:references>
    </xs:appinfo>
  </xs:annotation>
  <xs:element name=""GoList"">
    <xs:annotation>
      <xs:appinfo>
        <b:recordInfo body_xpath=""/*[local-name()='GoList' and namespace-uri()='http://ESB.Extensions.Schemas.GoList']"" />
      </xs:appinfo>
    </xs:annotation>
    <xs:complexType>
      <xs:sequence>
        <xs:element minOccurs=""0"" maxOccurs=""unbounded"" ref=""go:Go"" />
      </xs:sequence>
    </xs:complexType>
  </xs:element>
</xs:schema>";
        
        public GoList() {
        }
        
        public override string XmlContent {
            get {
                return _strSchema;
            }
        }
        
        public override string[] RootNodes {
            get {
                string[] _RootElements = new string [1];
                _RootElements[0] = "GoList";
                return _RootElements;
            }
        }
        
        protected override object RawSchema {
            get {
                return _rawSchema;
            }
            set {
                _rawSchema = value;
            }
        }
    }
}
