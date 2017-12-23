using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.XLANGs.BaseTypes;

using ESB.Extensions.Schemas;

namespace ESB.Extensions.Resolutions
{
    public partial class Go : Serializable<Go>
    {
        public static new Go CreateInstanceFromXml(string xml)
        {
            return Serializable<Go>.CreateInstanceFromXml(xml);
        }

        public Go()
        {
        }

        public Go(string batchId, string sequenceId)
            : this(batchId, sequenceId, true)
        {
        }

        public Go(string batchId, string sequenceId, bool shouldGenerateGoMsg)
        {
            this.BatchId = batchId;
            this.SequenceId = sequenceId;
            this.ShouldGenerateGoMsg = shouldGenerateGoMsg;
        }

        public Go(XLANGMessage msg)
            : this(msg, true)
        {
        }

        public Go(XLANGMessage msg, bool shouldGenerateGoMsg)
        {
            this.BatchId = (string) msg.GetPropertyValue(typeof(ESB.Extensions.Schemas.BatchId));
            this.SequenceId = (string)msg.GetPropertyValue(typeof(ESB.Extensions.Schemas.SequenceId));
            this.ShouldGenerateGoMsg = shouldGenerateGoMsg;
        }

        public override string ToString()
        {
            return string.Format("ESB.Extensions.Resolutions.Go; BatchId: {0}, SequenceId: {1}, ShouldGenerateGoMsg: {2}.", this.BatchId.ToString(), this.SequenceId.ToString(), this.ShouldGenerateGoMsg.ToString());
        }

        //public void IncrementSequenceId()
        //{
        //    ulong newSequenceId = Convert.ToUInt64(this.SequenceId) + 1;
        //    string format = new string('0', this.SequenceId.Length);
        //    this.SequenceId = newSequenceId.ToString(format);
        //}
    }
}
