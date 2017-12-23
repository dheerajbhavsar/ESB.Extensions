using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Modeling.ExtensionProvider.Metadata;
using Microsoft.Practices.Services.ItineraryDsl;
using Microsoft.Practices.Modeling.ExtensionProvider.Extension;

namespace ESB.Extensions.Itinerary.Extenders
{
    [ExtensionProviderAttribute("35A05F8E-44F3-461a-BDAC-3287E57DD296", "ReceivePipelineService", "ReceivePipelineService Itinerary Service Extension", typeof(ItineraryDslDomainModel))]
    [ItineraryExtensionProvider]
    public class ReceivePipelineServiceExtensionProvider : ExtensionProviderBase
    {
        public ReceivePipelineServiceExtensionProvider()
            : base(typeof(ReceivePipelineServiceExtender))
        {
        }
    }
}
