using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESB.Extensions.Resolutions
{
    public partial class ReceivePipelineServiceResolution : Serializable<ReceivePipelineServiceResolution>
    {
        public static new ReceivePipelineServiceResolution CreateInstanceFromXml(string xml)
        {
            return Serializable<ReceivePipelineServiceResolution>.CreateInstanceFromXml(xml);
        }

        public ReceivePipelineServiceResolution()
        {
        }

        public ReceivePipelineServiceResolution(string receivePipelineTypeName)
        {
            this.ReceivePipelineTypeName = receivePipelineTypeName;
        }

        public Type ReceivePipelineType
        {
            get { return Type.GetType(this.ReceivePipelineTypeName); }
        }
    }
}
