using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.RuleEngine;
using Microsoft.Practices.ESB.Resolver;
using System.Xml;

namespace ESB.Extensions.Resolutions
{
    public class SendPipelineServiceResolutionFactRetriever : IFactRetriever
    {
        public object UpdateFacts(RuleSetInfo ruleSetInfo, RuleEngine engine, object factsHandleIn)
        {
            return new SendPipelineServiceResolution();
        }
    }

    public class SendPipelineServiceResolutionFactCreator : IFactCreator
    {
        public object[] CreateFacts(RuleSetInfo ruleSetInfo)
        {
            object[] facts = new object[] { new XmlDocument(), new Resolution() };
            return facts;
        }

        public Type[] GetFactTypes(RuleSetInfo ruleSetInfo)
        {
            Type[] factTypes = new Type[] { typeof(XmlDocument), typeof(Resolution) };
            return factTypes;
        }
    }

 }
