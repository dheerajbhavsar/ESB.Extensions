namespace ESB.Extensions.Services {
    
    
    [Microsoft.XLANGs.BaseTypes.SchemaReference(@"ESB.Extensions.Schemas.EsbBatchDb.TypedProcedure_dbo+sp_GetAllCompletedBatchesResponse", typeof(global::ESB.Extensions.Schemas.EsbBatchDb.TypedProcedure_dbo.sp_GetAllCompletedBatchesResponse))]
    [Microsoft.XLANGs.BaseTypes.SchemaReference(@"ESB.Extensions.Schemas.GoList", typeof(global::ESB.Extensions.Schemas.GoList))]
    public sealed class T_GetAllCompletedBatchesResponseMsg_GoMsg : global::Microsoft.XLANGs.BaseTypes.TransformBase {
        
        private const string _strMap = @"<?xml version=""1.0"" encoding=""UTF-16""?>
<xsl:stylesheet xmlns:xsl=""http://www.w3.org/1999/XSL/Transform"" xmlns:msxsl=""urn:schemas-microsoft-com:xslt"" xmlns:var=""http://schemas.microsoft.com/BizTalk/2003/var"" exclude-result-prefixes=""msxsl var s0 s1"" version=""1.0"" xmlns:s0=""http://schemas.microsoft.com/Sql/2008/05/ProceduresResultSets/dbo/sp_GetAllCompletedBatches"" xmlns:go=""http://ESB.Extensions.Schemas.Go"" xmlns:s1=""http://schemas.microsoft.com/Sql/2008/05/TypedProcedures/dbo"" xmlns:ns0=""http://ESB.Extensions.Schemas.GoList"">
  <xsl:output omit-xml-declaration=""yes"" method=""xml"" version=""1.0"" />
  <xsl:template match=""/"">
    <xsl:apply-templates select=""/s1:sp_GetAllCompletedBatchesResponse"" />
  </xsl:template>
  <xsl:template match=""/s1:sp_GetAllCompletedBatchesResponse"">
    <ns0:GoList>
      <xsl:for-each select=""s1:StoredProcedureResultSet0"">
        <xsl:for-each select=""s0:StoredProcedureResultSet0"">
          <go:Go>
            <xsl:if test=""s0:BatchId"">
              <BatchId>
                <xsl:value-of select=""s0:BatchId/text()"" />
              </BatchId>
            </xsl:if>
            <xsl:if test=""s0:SequenceId"">
              <SequenceId>
                <xsl:value-of select=""s0:SequenceId/text()"" />
              </SequenceId>
            </xsl:if>
            <ShouldGenerateGoMsg>
              <xsl:text>true</xsl:text>
            </ShouldGenerateGoMsg>
            <xsl:value-of select=""./text()"" />
          </go:Go>
        </xsl:for-each>
      </xsl:for-each>
    </ns0:GoList>
  </xsl:template>
</xsl:stylesheet>";
        
        private const int _useXSLTransform = 0;
        
        private const string _strArgList = @"<ExtensionObjects />";
        
        private const string _strSrcSchemasList0 = @"ESB.Extensions.Schemas.EsbBatchDb.TypedProcedure_dbo+sp_GetAllCompletedBatchesResponse";
        
        private const global::ESB.Extensions.Schemas.EsbBatchDb.TypedProcedure_dbo.sp_GetAllCompletedBatchesResponse _srcSchemaTypeReference0 = null;
        
        private const string _strTrgSchemasList0 = @"ESB.Extensions.Schemas.GoList";
        
        private const global::ESB.Extensions.Schemas.GoList _trgSchemaTypeReference0 = null;
        
        public override string XmlContent {
            get {
                return _strMap;
            }
        }
        
        public override int UseXSLTransform {
            get {
                return _useXSLTransform;
            }
        }
        
        public override string XsltArgumentListContent {
            get {
                return _strArgList;
            }
        }
        
        public override string[] SourceSchemas {
            get {
                string[] _SrcSchemas = new string [1];
                _SrcSchemas[0] = @"ESB.Extensions.Schemas.EsbBatchDb.TypedProcedure_dbo+sp_GetAllCompletedBatchesResponse";
                return _SrcSchemas;
            }
        }
        
        public override string[] TargetSchemas {
            get {
                string[] _TrgSchemas = new string [1];
                _TrgSchemas[0] = @"ESB.Extensions.Schemas.GoList";
                return _TrgSchemas;
            }
        }
    }
}
