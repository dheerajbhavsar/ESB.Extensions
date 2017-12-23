using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.RuleEngine;

namespace ESB.Extensions.Resolution
{
    public class ResolutionDictionaryFactCreator : IFactCreator
    {
        public object[] CreateFacts(RuleSetInfo ruleSetInfo)
        {
            ResolutionDictionary rd = new ResolutionDictionary();
            rd.SetValue("ESB.Extensions.Schemas.BatchId", "000000");
            rd.SetValue("ESB.Extensions.Schemas.SequenceId", "000004");

            object[] facts = new object[] { rd };
            return facts;
        }

        public Type[] GetFactTypes(RuleSetInfo ruleSetInfo)
        {
            Type[] factTypes = new Type[] { typeof(ResolutionDictionary) };
            return factTypes;
        }
    }
}
