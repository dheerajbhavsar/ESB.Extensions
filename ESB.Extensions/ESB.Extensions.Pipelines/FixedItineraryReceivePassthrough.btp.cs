namespace ESB.Extensions.Pipelines
{
    using System;
    using System.Collections.Generic;
    using Microsoft.BizTalk.PipelineOM;
    using Microsoft.BizTalk.Component;
    using Microsoft.BizTalk.Component.Interop;
    
    
    public sealed class FixedItineraryReceivePassthrough : Microsoft.BizTalk.PipelineOM.ReceivePipeline
    {
        
        private const string _strPipeline = "<?xml version=\"1.0\" encoding=\"utf-16\"?><Document xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instanc"+
"e\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" MajorVersion=\"1\" MinorVersion=\"0\">  <Description /> "+
" <CategoryId>f66b9f5e-43ff-4f5f-ba46-885348ae1b4e</CategoryId>  <FriendlyName>Receive</FriendlyName>"+
"  <Stages>    <Stage>      <PolicyFileStage _locAttrData=\"Name\" _locID=\"1\" Name=\"Decode\" minOccurs=\""+
"0\" maxOccurs=\"-1\" execMethod=\"All\" stageId=\"9d0e4103-4cce-4536-83fa-4a5040674ad6\" />      <Component"+
"s>        <Component>          <Name>ESB.Extensions.PipelineConponents.ContextAdder.ContextAdder,ESB"+
".Extensions.PipelineComponents, Version=1.0.0.0, Culture=neutral, PublicKeyToken=70a94313e9b0b3df</N"+
"ame>          <ComponentName>ContextAdder</ComponentName>          <Description>Adds values to the m"+
"essage context</Description>          <Version>1.0.0.0</Version>          <Properties>            <P"+
"roperty Name=\"ContextPropertyInfoCollection\">              <Value xsi:type=\"xsd:string\">&lt;ContextP"+
"ropertyInfoCollection xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3"+
".org/2001/XMLSchema\" xmlns=\"urn:ESB/Extensions/PipelineConponents/ContextAdder\" /&gt;</Value>       "+
"     </Property>          </Properties>          <CachedDisplayName>ContextAdder</CachedDisplayName>"+
"          <CachedIsManaged>true</CachedIsManaged>        </Component>        <Component>          <N"+
"ame>Microsoft.Practices.ESB.Itinerary.PipelineComponents.Itinerary,Microsoft.Practices.ESB.Itinerary"+
".PipelineComponents, Version=2.1.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35</Name>       "+
"   <ComponentName>ESB Itinerary</ComponentName>          <Description>BizTalk ESB Itinerary Processe"+
"s Itinerary</Description>          <Version>2.0</Version>          <Properties />          <CachedDi"+
"splayName>ESB Itinerary</CachedDisplayName>          <CachedIsManaged>true</CachedIsManaged>        "+
"</Component>      </Components>    </Stage>    <Stage>      <PolicyFileStage _locAttrData=\"Name\" _lo"+
"cID=\"2\" Name=\"Disassemble\" minOccurs=\"0\" maxOccurs=\"-1\" execMethod=\"FirstMatch\" stageId=\"9d0e4105-4c"+
"ce-4536-83fa-4a5040674ad6\" />      <Components />    </Stage>    <Stage>      <PolicyFileStage _locA"+
"ttrData=\"Name\" _locID=\"3\" Name=\"Validate\" minOccurs=\"0\" maxOccurs=\"-1\" execMethod=\"All\" stageId=\"9d0"+
"e410d-4cce-4536-83fa-4a5040674ad6\" />      <Components>        <Component>          <Name>Microsoft."+
"Practices.ESB.PipelineComponents.Dispatcher,Microsoft.Practices.ESB.PipelineComponents, Version=2.1."+
"0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35</Name>          <ComponentName>ESB Dispatcher<"+
"/ComponentName>          <Description>BizTalk ESB Dispatcher Processes Itinerary, Routing and Transf"+
"orm  Requests</Description>          <Version>2.0</Version>          <Properties>            <Proper"+
"ty Name=\"Enabled\">              <Value xsi:type=\"xsd:boolean\">true</Value>            </Property>   "+
"         <Property Name=\"Endpoint\">              <Value xsi:type=\"xsd:string\" />            </Proper"+
"ty>            <Property Name=\"MapName\">              <Value xsi:type=\"xsd:string\" />            </P"+
"roperty>            <Property Name=\"Validate\">              <Value xsi:type=\"xsd:boolean\">true</Valu"+
"e>            </Property>            <Property Name=\"RoutingServiceName\">              <Value xsi:ty"+
"pe=\"xsd:string\">Microsoft.Practices.ESB.Services.Routing</Value>            </Property>            <"+
"Property Name=\"TransformServiceName\">              <Value xsi:type=\"xsd:string\">Microsoft.Practices."+
"ESB.Services.Transform</Value>            </Property>          </Properties>          <CachedDisplay"+
"Name>ESB Dispatcher</CachedDisplayName>          <CachedIsManaged>true</CachedIsManaged>        </Co"+
"mponent>      </Components>    </Stage>    <Stage>      <PolicyFileStage _locAttrData=\"Name\" _locID="+
"\"4\" Name=\"ResolveParty\" minOccurs=\"0\" maxOccurs=\"-1\" execMethod=\"All\" stageId=\"9d0e410e-4cce-4536-83"+
"fa-4a5040674ad6\" />      <Components />    </Stage>  </Stages></Document>";
        
        private const string _versionDependentGuid = "b668d985-2b0b-49a3-b9f8-fca9f95c80a9";
        
        public FixedItineraryReceivePassthrough()
        {
            Microsoft.BizTalk.PipelineOM.Stage stage = this.AddStage(new System.Guid("9d0e4103-4cce-4536-83fa-4a5040674ad6"), Microsoft.BizTalk.PipelineOM.ExecutionMode.all);
            IBaseComponent comp0 = Microsoft.BizTalk.PipelineOM.PipelineManager.CreateComponent("ESB.Extensions.PipelineConponents.ContextAdder.ContextAdder,ESB.Extensions.PipelineComponents, Version=1.0.0.0, Culture=neutral, PublicKeyToken=70a94313e9b0b3df");;
            if (comp0 is IPersistPropertyBag)
            {
                string comp0XmlProperties = "<?xml version=\"1.0\" encoding=\"utf-16\"?><PropertyBag xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-inst"+
"ance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">  <Properties>    <Property Name=\"ContextProperty"+
"InfoCollection\">      <Value xsi:type=\"xsd:string\">&lt;ContextPropertyInfoCollection xmlns:xsi=\"http"+
"://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"urn:ESB/E"+
"xtensions/PipelineConponents/ContextAdder\" /&gt;</Value>    </Property>  </Properties></PropertyBag>";
                PropertyBag pb = PropertyBag.DeserializeFromXml(comp0XmlProperties);;
                ((IPersistPropertyBag)(comp0)).Load(pb, 0);
            }
            this.AddComponent(stage, comp0);
            IBaseComponent comp1 = Microsoft.BizTalk.PipelineOM.PipelineManager.CreateComponent("Microsoft.Practices.ESB.Itinerary.PipelineComponents.Itinerary,Microsoft.Practices.ESB.Itinerary.PipelineComponents, Version=2.1.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35");;
            if (comp1 is IPersistPropertyBag)
            {
                string comp1XmlProperties = "<?xml version=\"1.0\" encoding=\"utf-16\"?><PropertyBag xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-inst"+
"ance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">  <Properties /></PropertyBag>";
                PropertyBag pb = PropertyBag.DeserializeFromXml(comp1XmlProperties);;
                ((IPersistPropertyBag)(comp1)).Load(pb, 0);
            }
            this.AddComponent(stage, comp1);
            stage = this.AddStage(new System.Guid("9d0e410d-4cce-4536-83fa-4a5040674ad6"), Microsoft.BizTalk.PipelineOM.ExecutionMode.all);
            IBaseComponent comp2 = Microsoft.BizTalk.PipelineOM.PipelineManager.CreateComponent("Microsoft.Practices.ESB.PipelineComponents.Dispatcher,Microsoft.Practices.ESB.PipelineComponents, Version=2.1.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35");;
            if (comp2 is IPersistPropertyBag)
            {
                string comp2XmlProperties = "<?xml version=\"1.0\" encoding=\"utf-16\"?><PropertyBag xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-inst"+
"ance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">  <Properties>    <Property Name=\"Enabled\">      "+
"<Value xsi:type=\"xsd:boolean\">true</Value>    </Property>    <Property Name=\"Endpoint\">      <Value "+
"xsi:type=\"xsd:string\" />    </Property>    <Property Name=\"MapName\">      <Value xsi:type=\"xsd:strin"+
"g\" />    </Property>    <Property Name=\"Validate\">      <Value xsi:type=\"xsd:boolean\">true</Value>  "+
"  </Property>    <Property Name=\"RoutingServiceName\">      <Value xsi:type=\"xsd:string\">Microsoft.Pr"+
"actices.ESB.Services.Routing</Value>    </Property>    <Property Name=\"TransformServiceName\">      <"+
"Value xsi:type=\"xsd:string\">Microsoft.Practices.ESB.Services.Transform</Value>    </Property>  </Pro"+
"perties></PropertyBag>";
                PropertyBag pb = PropertyBag.DeserializeFromXml(comp2XmlProperties);;
                ((IPersistPropertyBag)(comp2)).Load(pb, 0);
            }
            this.AddComponent(stage, comp2);
        }
        
        public override string XmlContent
        {
            get
            {
                return _strPipeline;
            }
        }
        
        public override System.Guid VersionDependentGuid
        {
            get
            {
                return new System.Guid(_versionDependentGuid);
            }
        }
    }
}
