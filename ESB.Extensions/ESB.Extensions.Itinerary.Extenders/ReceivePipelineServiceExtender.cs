using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Drawing.Design;
using Microsoft.Practices.Modeling.ExtensionProvider.Metadata;
using Microsoft.Practices.Services.ItineraryDsl;
using Microsoft.Practices.Modeling.ExtensionProvider.Extension;
using Microsoft.Practices.Modeling.Common;
using Microsoft.Practices.Modeling.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using Microsoft.Practices.Services.Extenders;
using Microsoft.Practices.Modeling.Services.Design;
using Microsoft.Practices.Modeling.Common.Design;

namespace ESB.Extensions.Itinerary.Extenders
{
    [Serializable]
    [ObjectExtender(typeof(ItineraryService))]
    public class ReceivePipelineServiceExtender : ItineraryServiceExtenderBase, IOrchestrationItineraryServiceExtender
    {
        [Editor(typeof(BiztalkOrchestrationServiceNameEditor), typeof(UITypeEditor))]
        [TypeConverter(typeof(TypeConverter))]
        [EditorOutputProperty(BiztalkOrchestrationServiceNameEditor.ServiceId, "ServiceId")]
        public override string ServiceName { get; set; }

        public override string ServiceType
        {
            get { return "Orchestration"; }
        }
    }
}
