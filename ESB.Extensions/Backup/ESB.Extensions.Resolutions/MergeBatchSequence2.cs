using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESB.Extensions.Resolutions
{
    public partial class sp_MergeBatchSequence : Serializable<sp_MergeBatchSequence>
    {
        public static new sp_MergeBatchSequence CreateInstanceFromXml(string xml)
        {
            return Serializable<sp_MergeBatchSequence>.CreateInstanceFromXml(xml);
        }

        public sp_MergeBatchSequence()
        {
        }

        public sp_MergeBatchSequence(string batchId, string sequenceId)
        {
            this.BatchId = batchId;
            this.SequenceId = sequenceId;
        }

        public sp_MergeBatchSequence(string batchId, string sequenceId, bool? deterministicSequenceIds, bool? isFirstSequenceId, int? numberOfMessages)
            : this(batchId, sequenceId)
        {
            this.DeterministicSequenceIds = deterministicSequenceIds;
            if (deterministicSequenceIds.HasValue)
            {
                this.DeterministicSequenceIdsSpecified = true;
            }

            this.IsFirstSequenceId = isFirstSequenceId;
            if (isFirstSequenceId.HasValue)
            {
                this.IsFirstSequenceIdSpecified = true;
            }

            this.NumberOfMessages = numberOfMessages;
            if (numberOfMessages.HasValue)
            {
                this.NumberOfMessagesSpecified = true;
            }
        }
    }
}
