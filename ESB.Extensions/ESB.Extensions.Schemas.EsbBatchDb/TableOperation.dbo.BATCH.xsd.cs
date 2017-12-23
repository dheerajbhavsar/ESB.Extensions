namespace ESB.Extensions.Schemas.EsbBatchDb {
    using Microsoft.XLANGs.BaseTypes;
    
    
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.BizTalk.Schema.Compiler", "3.0.1.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [SchemaType(SchemaTypeEnum.Document)]
    [System.SerializableAttribute()]
    [SchemaRoots(new string[] {@"Delete", @"DeleteResponse", @"Insert", @"InsertResponse", @"Select", @"SelectResponse", @"RowPair", @"ArrayOfRowPair", @"Update", @"UpdateResponse"})]
    [Microsoft.XLANGs.BaseTypes.SchemaReference(@"ESB.Extensions.Schemas.WcfSql.SimpleTypeArray", typeof(global::ESB.Extensions.Schemas.WcfSql.SimpleTypeArray))]
    [Microsoft.XLANGs.BaseTypes.SchemaReference(@"ESB.Extensions.Schemas.EsbBatchDb.Table_dbo", typeof(global::ESB.Extensions.Schemas.EsbBatchDb.Table_dbo))]
    public sealed class TableOperation_dbo_BATCH : Microsoft.XLANGs.BaseTypes.SchemaBase {
        
        [System.NonSerializedAttribute()]
        private static object _rawSchema;
        
        [System.NonSerializedAttribute()]
        private const string _strSchema = @"<?xml version=""1.0"" encoding=""utf-16""?>
<xs:schema xmlns:array=""http://schemas.microsoft.com/2003/10/Serialization/Arrays"" xmlns=""http://schemas.microsoft.com/Sql/2008/05/TableOp/dbo/BATCH"" xmlns:ns3=""http://schemas.microsoft.com/Sql/2008/05/Types/Tables/dbo"" xmlns:b=""http://schemas.microsoft.com/BizTalk/2003"" elementFormDefault=""qualified"" targetNamespace=""http://schemas.microsoft.com/Sql/2008/05/TableOp/dbo/BATCH"" version=""1.0"" xmlns:xs=""http://www.w3.org/2001/XMLSchema"">
  <xs:import schemaLocation=""ESB.Extensions.Schemas.WcfSql.SimpleTypeArray"" namespace=""http://schemas.microsoft.com/2003/10/Serialization/Arrays"" />
  <xs:import schemaLocation=""ESB.Extensions.Schemas.EsbBatchDb.Table_dbo"" namespace=""http://schemas.microsoft.com/Sql/2008/05/Types/Tables/dbo"" />
  <xs:annotation>
    <xs:appinfo>
      <fileNameHint xmlns=""http://schemas.microsoft.com/servicemodel/adapters/metadata/xsd"">TableOperation.dbo.BATCH</fileNameHint>
      <b:references>
        <b:reference targetNamespace=""http://schemas.microsoft.com/Sql/2008/05/Types/Tables/dbo"" />
        <b:reference targetNamespace=""http://schemas.microsoft.com/2003/10/Serialization/Arrays"" />
      </b:references>
    </xs:appinfo>
  </xs:annotation>
  <xs:element name=""Delete"">
    <xs:annotation>
      <xs:documentation>
        <doc:action xmlns:doc=""http://schemas.microsoft.com/servicemodel/adapters/metadata/documentation"">TableOp/Delete/dbo/BATCH</doc:action>
      </xs:documentation>
    </xs:annotation>
    <xs:complexType>
      <xs:sequence>
        <xs:element minOccurs=""0"" maxOccurs=""1"" name=""Rows"" nillable=""true"" type=""ns3:ArrayOfBATCH"" />
      </xs:sequence>
    </xs:complexType>
  </xs:element>
  <xs:element name=""DeleteResponse"">
    <xs:annotation>
      <xs:documentation>
        <doc:action xmlns:doc=""http://schemas.microsoft.com/servicemodel/adapters/metadata/documentation"">TableOp/Delete/dbo/BATCH/response</doc:action>
      </xs:documentation>
    </xs:annotation>
    <xs:complexType>
      <xs:sequence>
        <xs:element minOccurs=""1"" maxOccurs=""1"" name=""DeleteResult"" type=""xs:int"" />
      </xs:sequence>
    </xs:complexType>
  </xs:element>
  <xs:element name=""Insert"">
    <xs:annotation>
      <xs:documentation>
        <doc:action xmlns:doc=""http://schemas.microsoft.com/servicemodel/adapters/metadata/documentation"">TableOp/Insert/dbo/BATCH</doc:action>
      </xs:documentation>
    </xs:annotation>
    <xs:complexType>
      <xs:sequence>
        <xs:element minOccurs=""0"" maxOccurs=""1"" name=""Rows"" nillable=""true"" type=""ns3:ArrayOfBATCH"" />
      </xs:sequence>
    </xs:complexType>
  </xs:element>
  <xs:element name=""InsertResponse"">
    <xs:annotation>
      <xs:documentation>
        <doc:action xmlns:doc=""http://schemas.microsoft.com/servicemodel/adapters/metadata/documentation"">TableOp/Insert/dbo/BATCH/response</doc:action>
      </xs:documentation>
    </xs:annotation>
    <xs:complexType>
      <xs:sequence>
        <xs:element minOccurs=""0"" maxOccurs=""1"" name=""InsertResult"" nillable=""true"" type=""array:ArrayOflong"" />
      </xs:sequence>
    </xs:complexType>
  </xs:element>
  <xs:element name=""Select"">
    <xs:annotation>
      <xs:documentation>
        <doc:action xmlns:doc=""http://schemas.microsoft.com/servicemodel/adapters/metadata/documentation"">TableOp/Select/dbo/BATCH</doc:action>
      </xs:documentation>
    </xs:annotation>
    <xs:complexType>
      <xs:sequence>
        <xs:element minOccurs=""1"" maxOccurs=""1"" name=""Columns"" nillable=""true"" type=""xs:string"" />
        <xs:element minOccurs=""0"" maxOccurs=""1"" name=""Query"" nillable=""true"" type=""xs:string"" />
      </xs:sequence>
    </xs:complexType>
  </xs:element>
  <xs:element name=""SelectResponse"">
    <xs:annotation>
      <xs:documentation>
        <doc:action xmlns:doc=""http://schemas.microsoft.com/servicemodel/adapters/metadata/documentation"">TableOp/Select/dbo/BATCH/response</doc:action>
      </xs:documentation>
    </xs:annotation>
    <xs:complexType>
      <xs:sequence>
        <xs:element minOccurs=""0"" maxOccurs=""1"" name=""SelectResult"" nillable=""true"" type=""ns3:ArrayOfBATCH"" />
      </xs:sequence>
    </xs:complexType>
  </xs:element>
  <xs:complexType name=""RowPair"">
    <xs:sequence>
      <xs:element minOccurs=""1"" maxOccurs=""1"" name=""After"" nillable=""true"" type=""ns3:BATCH"" />
      <xs:element minOccurs=""1"" maxOccurs=""1"" name=""Before"" nillable=""true"" type=""ns3:BATCH"" />
    </xs:sequence>
  </xs:complexType>
  <xs:element xmlns:q1=""http://schemas.microsoft.com/Sql/2008/05/TableOp/dbo/BATCH"" name=""RowPair"" nillable=""true"" type=""q1:RowPair"" />
  <xs:complexType name=""ArrayOfRowPair"">
    <xs:sequence>
      <xs:element xmlns:q2=""http://schemas.microsoft.com/Sql/2008/05/TableOp/dbo/BATCH"" minOccurs=""0"" maxOccurs=""unbounded"" name=""RowPair"" type=""q2:RowPair"" />
    </xs:sequence>
  </xs:complexType>
  <xs:element xmlns:q3=""http://schemas.microsoft.com/Sql/2008/05/TableOp/dbo/BATCH"" name=""ArrayOfRowPair"" nillable=""true"" type=""q3:ArrayOfRowPair"" />
  <xs:element name=""Update"">
    <xs:annotation>
      <xs:documentation>
        <doc:action xmlns:doc=""http://schemas.microsoft.com/servicemodel/adapters/metadata/documentation"">TableOp/Update/dbo/BATCH</doc:action>
      </xs:documentation>
    </xs:annotation>
    <xs:complexType>
      <xs:sequence>
        <xs:element xmlns:q4=""http://schemas.microsoft.com/Sql/2008/05/TableOp/dbo/BATCH"" minOccurs=""0"" maxOccurs=""1"" name=""Rows"" nillable=""true"" type=""q4:ArrayOfRowPair"" />
      </xs:sequence>
    </xs:complexType>
  </xs:element>
  <xs:element name=""UpdateResponse"">
    <xs:annotation>
      <xs:documentation>
        <doc:action xmlns:doc=""http://schemas.microsoft.com/servicemodel/adapters/metadata/documentation"">TableOp/Update/dbo/BATCH/response</doc:action>
      </xs:documentation>
    </xs:annotation>
    <xs:complexType>
      <xs:sequence>
        <xs:element minOccurs=""1"" maxOccurs=""1"" name=""UpdateResult"" type=""xs:int"" />
      </xs:sequence>
    </xs:complexType>
  </xs:element>
</xs:schema>";
        
        private const global::ESB.Extensions.Schemas.WcfSql.SimpleTypeArray  __DummyVar0 = null;
        
        public TableOperation_dbo_BATCH() {
        }
        
        public override string XmlContent {
            get {
                return _strSchema;
            }
        }
        
        public override string[] RootNodes {
            get {
                string[] _RootElements = new string [10];
                _RootElements[0] = "Delete";
                _RootElements[1] = "DeleteResponse";
                _RootElements[2] = "Insert";
                _RootElements[3] = "InsertResponse";
                _RootElements[4] = "Select";
                _RootElements[5] = "SelectResponse";
                _RootElements[6] = "RowPair";
                _RootElements[7] = "ArrayOfRowPair";
                _RootElements[8] = "Update";
                _RootElements[9] = "UpdateResponse";
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
        
        [Schema(@"http://schemas.microsoft.com/Sql/2008/05/TableOp/dbo/BATCH",@"Delete")]
        [System.SerializableAttribute()]
        [SchemaRoots(new string[] {@"Delete"})]
        public sealed class Delete : Microsoft.XLANGs.BaseTypes.SchemaBase {
            
            [System.NonSerializedAttribute()]
            private static object _rawSchema;
            
            public Delete() {
            }
            
            public override string XmlContent {
                get {
                    return _strSchema;
                }
            }
            
            public override string[] RootNodes {
                get {
                    string[] _RootElements = new string [1];
                    _RootElements[0] = "Delete";
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
        
        [Schema(@"http://schemas.microsoft.com/Sql/2008/05/TableOp/dbo/BATCH",@"DeleteResponse")]
        [System.SerializableAttribute()]
        [SchemaRoots(new string[] {@"DeleteResponse"})]
        public sealed class DeleteResponse : Microsoft.XLANGs.BaseTypes.SchemaBase {
            
            [System.NonSerializedAttribute()]
            private static object _rawSchema;
            
            public DeleteResponse() {
            }
            
            public override string XmlContent {
                get {
                    return _strSchema;
                }
            }
            
            public override string[] RootNodes {
                get {
                    string[] _RootElements = new string [1];
                    _RootElements[0] = "DeleteResponse";
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
        
        [Schema(@"http://schemas.microsoft.com/Sql/2008/05/TableOp/dbo/BATCH",@"Insert")]
        [System.SerializableAttribute()]
        [SchemaRoots(new string[] {@"Insert"})]
        public sealed class Insert : Microsoft.XLANGs.BaseTypes.SchemaBase {
            
            [System.NonSerializedAttribute()]
            private static object _rawSchema;
            
            public Insert() {
            }
            
            public override string XmlContent {
                get {
                    return _strSchema;
                }
            }
            
            public override string[] RootNodes {
                get {
                    string[] _RootElements = new string [1];
                    _RootElements[0] = "Insert";
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
        
        [Schema(@"http://schemas.microsoft.com/Sql/2008/05/TableOp/dbo/BATCH",@"InsertResponse")]
        [System.SerializableAttribute()]
        [SchemaRoots(new string[] {@"InsertResponse"})]
        public sealed class InsertResponse : Microsoft.XLANGs.BaseTypes.SchemaBase {
            
            [System.NonSerializedAttribute()]
            private static object _rawSchema;
            
            public InsertResponse() {
            }
            
            public override string XmlContent {
                get {
                    return _strSchema;
                }
            }
            
            public override string[] RootNodes {
                get {
                    string[] _RootElements = new string [1];
                    _RootElements[0] = "InsertResponse";
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
        
        [Schema(@"http://schemas.microsoft.com/Sql/2008/05/TableOp/dbo/BATCH",@"Select")]
        [System.SerializableAttribute()]
        [SchemaRoots(new string[] {@"Select"})]
        public sealed class Select : Microsoft.XLANGs.BaseTypes.SchemaBase {
            
            [System.NonSerializedAttribute()]
            private static object _rawSchema;
            
            public Select() {
            }
            
            public override string XmlContent {
                get {
                    return _strSchema;
                }
            }
            
            public override string[] RootNodes {
                get {
                    string[] _RootElements = new string [1];
                    _RootElements[0] = "Select";
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
        
        [Schema(@"http://schemas.microsoft.com/Sql/2008/05/TableOp/dbo/BATCH",@"SelectResponse")]
        [System.SerializableAttribute()]
        [SchemaRoots(new string[] {@"SelectResponse"})]
        public sealed class SelectResponse : Microsoft.XLANGs.BaseTypes.SchemaBase {
            
            [System.NonSerializedAttribute()]
            private static object _rawSchema;
            
            public SelectResponse() {
            }
            
            public override string XmlContent {
                get {
                    return _strSchema;
                }
            }
            
            public override string[] RootNodes {
                get {
                    string[] _RootElements = new string [1];
                    _RootElements[0] = "SelectResponse";
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
        
        [Schema(@"http://schemas.microsoft.com/Sql/2008/05/TableOp/dbo/BATCH",@"RowPair")]
        [System.SerializableAttribute()]
        [SchemaRoots(new string[] {@"RowPair"})]
        public sealed class RowPair : Microsoft.XLANGs.BaseTypes.SchemaBase {
            
            [System.NonSerializedAttribute()]
            private static object _rawSchema;
            
            public RowPair() {
            }
            
            public override string XmlContent {
                get {
                    return _strSchema;
                }
            }
            
            public override string[] RootNodes {
                get {
                    string[] _RootElements = new string [1];
                    _RootElements[0] = "RowPair";
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
        
        [Schema(@"http://schemas.microsoft.com/Sql/2008/05/TableOp/dbo/BATCH",@"ArrayOfRowPair")]
        [System.SerializableAttribute()]
        [SchemaRoots(new string[] {@"ArrayOfRowPair"})]
        public sealed class ArrayOfRowPair : Microsoft.XLANGs.BaseTypes.SchemaBase {
            
            [System.NonSerializedAttribute()]
            private static object _rawSchema;
            
            public ArrayOfRowPair() {
            }
            
            public override string XmlContent {
                get {
                    return _strSchema;
                }
            }
            
            public override string[] RootNodes {
                get {
                    string[] _RootElements = new string [1];
                    _RootElements[0] = "ArrayOfRowPair";
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
        
        [Schema(@"http://schemas.microsoft.com/Sql/2008/05/TableOp/dbo/BATCH",@"Update")]
        [System.SerializableAttribute()]
        [SchemaRoots(new string[] {@"Update"})]
        public sealed class Update : Microsoft.XLANGs.BaseTypes.SchemaBase {
            
            [System.NonSerializedAttribute()]
            private static object _rawSchema;
            
            public Update() {
            }
            
            public override string XmlContent {
                get {
                    return _strSchema;
                }
            }
            
            public override string[] RootNodes {
                get {
                    string[] _RootElements = new string [1];
                    _RootElements[0] = "Update";
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
        
        [Schema(@"http://schemas.microsoft.com/Sql/2008/05/TableOp/dbo/BATCH",@"UpdateResponse")]
        [System.SerializableAttribute()]
        [SchemaRoots(new string[] {@"UpdateResponse"})]
        public sealed class UpdateResponse : Microsoft.XLANGs.BaseTypes.SchemaBase {
            
            [System.NonSerializedAttribute()]
            private static object _rawSchema;
            
            public UpdateResponse() {
            }
            
            public override string XmlContent {
                get {
                    return _strSchema;
                }
            }
            
            public override string[] RootNodes {
                get {
                    string[] _RootElements = new string [1];
                    _RootElements[0] = "UpdateResponse";
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
}
