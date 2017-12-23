namespace ESB.Extensions.Schemas {
    using Microsoft.XLANGs.BaseTypes;
    
    
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.BizTalk.Schema.Compiler", "3.0.1.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [SchemaType(SchemaTypeEnum.Document)]
    [Schema(@"http://ESB.Extensions.Schemas.Go",@"Go")]
    [Microsoft.XLANGs.BaseTypes.DistinguishedFieldAttribute(typeof(System.String), "BatchId", XPath = @"/*[local-name()='Go' and namespace-uri()='http://ESB.Extensions.Schemas.Go']/*[local-name()='BatchId' and namespace-uri()='']", XsdType = @"string")]
    [Microsoft.XLANGs.BaseTypes.DistinguishedFieldAttribute(typeof(System.String), "SequenceId", XPath = @"/*[local-name()='Go' and namespace-uri()='http://ESB.Extensions.Schemas.Go']/*[local-name()='SequenceId' and namespace-uri()='']", XsdType = @"string")]
    [Microsoft.XLANGs.BaseTypes.PropertyAttribute(typeof(global::ESB.Extensions.Schemas.BatchId), XPath = @"/*[local-name()='Go' and namespace-uri()='http://ESB.Extensions.Schemas.Go']/*[local-name()='BatchId' and namespace-uri()='']", XsdType = @"string")]
    [Microsoft.XLANGs.BaseTypes.PropertyAttribute(typeof(global::ESB.Extensions.Schemas.SequenceId), XPath = @"/*[local-name()='Go' and namespace-uri()='http://ESB.Extensions.Schemas.Go']/*[local-name()='SequenceId' and namespace-uri()='']", XsdType = @"string")]
    [Microsoft.XLANGs.BaseTypes.DistinguishedFieldAttribute(typeof(System.Boolean), "ShouldGenerateGoMsg", XPath = @"/*[local-name()='Go' and namespace-uri()='http://ESB.Extensions.Schemas.Go']/*[local-name()='ShouldGenerateGoMsg' and namespace-uri()='']", XsdType = @"boolean")]
    [System.SerializableAttribute()]
    [SchemaRoots(new string[] {@"Go"})]
    [Microsoft.XLANGs.BaseTypes.SchemaReference(@"ESB.Extensions.Schemas.Properties", typeof(global::ESB.Extensions.Schemas.Properties))]
    public sealed class Go : Microsoft.XLANGs.BaseTypes.SchemaBase {
        
        [System.NonSerializedAttribute()]
        private static object _rawSchema;
        
        [System.NonSerializedAttribute()]
        private const string _strSchema = @"<?xml version=""1.0"" encoding=""utf-16""?>
<xs:schema xmlns=""http://ESB.Extensions.Schemas.Go"" xmlns:b=""http://schemas.microsoft.com/BizTalk/2003"" xmlns:ns0=""https://ESB.Extensions.Schemas.Properties"" targetNamespace=""http://ESB.Extensions.Schemas.Go"" xmlns:xs=""http://www.w3.org/2001/XMLSchema"">
  <xs:annotation>
    <xs:appinfo>
      <b:imports>
        <b:namespace prefix=""ns0"" uri=""https://ESB.Extensions.Schemas.Properties"" location=""ESB.Extensions.Schemas.Properties"" />
      </b:imports>
    </xs:appinfo>
  </xs:annotation>
  <xs:element name=""Go"">
    <xs:annotation>
      <xs:appinfo>
        <b:properties>
          <b:property distinguished=""true"" xpath=""/*[local-name()='Go' and namespace-uri()='http://ESB.Extensions.Schemas.Go']/*[local-name()='BatchId' and namespace-uri()='']"" />
          <b:property distinguished=""true"" xpath=""/*[local-name()='Go' and namespace-uri()='http://ESB.Extensions.Schemas.Go']/*[local-name()='SequenceId' and namespace-uri()='']"" />
          <b:property name=""ns0:BatchId"" xpath=""/*[local-name()='Go' and namespace-uri()='http://ESB.Extensions.Schemas.Go']/*[local-name()='BatchId' and namespace-uri()='']"" />
          <b:property name=""ns0:SequenceId"" xpath=""/*[local-name()='Go' and namespace-uri()='http://ESB.Extensions.Schemas.Go']/*[local-name()='SequenceId' and namespace-uri()='']"" />
          <b:property distinguished=""true"" xpath=""/*[local-name()='Go' and namespace-uri()='http://ESB.Extensions.Schemas.Go']/*[local-name()='ShouldGenerateGoMsg' and namespace-uri()='']"" />
        </b:properties>
      </xs:appinfo>
    </xs:annotation>
    <xs:complexType>
      <xs:sequence>
        <xs:element name=""BatchId"" type=""xs:string"" />
        <xs:element name=""SequenceId"" type=""xs:string"" />
        <xs:element name=""ShouldGenerateGoMsg"" type=""xs:boolean"" />
      </xs:sequence>
    </xs:complexType>
  </xs:element>
</xs:schema>";
        
        public Go() {
        }
        
        public override string XmlContent {
            get {
                return _strSchema;
            }
        }
        
        public override string[] RootNodes {
            get {
                string[] _RootElements = new string [1];
                _RootElements[0] = "Go";
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
