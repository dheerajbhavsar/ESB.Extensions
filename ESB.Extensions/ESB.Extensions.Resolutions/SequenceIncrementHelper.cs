using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESB.Extensions.Resolutions
{
    public class SequenceIncrementHelper
    {
        public SequenceIncrementHelper()
        {
            System.Diagnostics.Debug.WriteLine(string.Format("SequenceIncrementHelper; HashCode: {0}", this.GetHashCode()));
        }

        public void IncrementSequenceId(Go go)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("SequenceIncrementHelper.IncrementSequenceId on HashCode: {0}; {1}", this.GetHashCode(), go.ToString()));

            string sequenceId = go.SequenceId;
            ulong newSequenceId = Convert.ToUInt64(sequenceId) + 1;

            string format = new string('0', sequenceId.Length);
            go.SequenceId = newSequenceId.ToString(format);
        }

        public int ConvertBatchIdToInt32(Go go)
        {
            return System.Convert.ToInt32(go.BatchId);
        }

        public int ConvertSequenceIdToInt32(Go go)
        {
            return System.Convert.ToInt32(go.SequenceId);
        }
    }
}
