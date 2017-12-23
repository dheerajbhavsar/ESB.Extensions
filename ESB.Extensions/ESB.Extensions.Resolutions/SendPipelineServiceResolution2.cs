using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.XLANGs.BaseTypes;

namespace ESB.Extensions.Resolutions
{
    public partial class SendPipelineServiceResolution : Serializable<SendPipelineServiceResolution>
    {
        public static new SendPipelineServiceResolution CreateInstanceFromXml(string xml)
        {
            return Serializable<SendPipelineServiceResolution>.CreateInstanceFromXml(xml);
        }

        public SendPipelineServiceResolution()
        {
            this.BatchTimeout = new SendPipelineServiceResolutionBatchTimeout();
            this.MessageTimeout = new SendPipelineServiceResolutionMessageTimeout();
        }

        public SendPipelineServiceResolution(string sendPipelineTypeName, string xlangMessageComparerTypeName, int maxMessageCount, int batchTimeoutDays, int messageTimeoutDays)
            : this()
        {
            this.SendPipelineTypeName = sendPipelineTypeName;
            this.XLANGMessageComparerTypeName = xlangMessageComparerTypeName;
            this.MaxMessageCount = maxMessageCount;
            this.SetBatchTimeoutDays(batchTimeoutDays);
            this.SetMessageTimeoutDays(messageTimeoutDays);
        }

        public SendPipelineServiceResolution(string sendPipelineTypeName, string xlangMessageComparerTypeName, int maxMessageCount, int batchTimeoutDays, int batchTimeoutHours, int batchTimeoutMinutes, int batchTimeoutSeconds, bool batchTimeoutThrow, int messageTimeoutDays, int messageTimeoutHours, int messageTimeoutMinutes, int messageTimeoutSeconds, bool messageTimeoutThrow)
            : this()
        {
            this.SendPipelineTypeName = sendPipelineTypeName;
            this.XLANGMessageComparerTypeName = xlangMessageComparerTypeName;
            this.MaxMessageCount = maxMessageCount;
            this.SetBatchTimeoutDays(batchTimeoutDays);
            this.SetBatchTimeoutHours(batchTimeoutHours);
            this.SetBatchTimeoutMinutes(batchTimeoutMinutes);
            this.SetBatchTimeoutSeconds(batchTimeoutSeconds);
            this.SetMessageTimeoutDays(messageTimeoutDays);
            this.SetMessageTimeoutHours(messageTimeoutHours);
            this.SetMessageTimeoutMinutes(messageTimeoutMinutes);
            this.SetMessageTimeoutSeconds(messageTimeoutSeconds);
        }

        public Type SendPipelineType
        {
            get { return Type.GetType(this.SendPipelineTypeName); }
        }

        private IComparer<XLANGMessage> _xlangMessageComparerInstance;
        public IComparer<XLANGMessage> XLANGMessageComparerInstance
        {
            get
            {
                if ((null == _xlangMessageComparerInstance) && (!string.IsNullOrEmpty(this.XLANGMessageComparerTypeName)))
                {
                    _xlangMessageComparerInstance = (IComparer<XLANGMessage>) Activator.CreateInstance(Type.GetType(this.XLANGMessageComparerTypeName));
                }
                return _xlangMessageComparerInstance;
            }
        }

        public void SetBatchTimeoutDays(int days)
        {
            this.BatchTimeout.Days = days;
            this.BatchTimeout.DaysSpecified = true;
        }

        public void SetBatchTimeoutHours(int hours)
        {
            this.BatchTimeout.Hours = hours;
            this.BatchTimeout.HoursSpecified = true;
        }

        public void SetBatchTimeoutMinutes(int minutes)
        {
            this.BatchTimeout.Minutes = minutes;
            this.BatchTimeout.MinutesSpecified = true;
        }

        public void SetBatchTimeoutSeconds(int seconds)
        {
            this.BatchTimeout.Seconds = seconds;
            this.BatchTimeout.SecondsSpecified = true;
        }

        public void SetBatchTimeoutThrow(bool throwOnTimeout)
        {
            this.BatchTimeout.ThrowExceptionOnTimeout = throwOnTimeout;
            this.BatchTimeout.ThrowExceptionOnTimeoutSpecified = true;
        }

        public void SetMessageTimeoutDays(int days)
        {
            this.MessageTimeout.Days = days;
            this.MessageTimeout.DaysSpecified = true;
        }

        public void SetMessageTimeoutHours(int hours)
        {
            this.MessageTimeout.Hours = hours;
            this.MessageTimeout.HoursSpecified = true;
        }

        public void SetMessageTimeoutMinutes(int minutes)
        {
            this.MessageTimeout.Minutes = minutes;
            this.MessageTimeout.MinutesSpecified = true;
        }

        public void SetMessageTimeoutSeconds(int seconds)
        {
            this.MessageTimeout.Seconds = seconds;
            this.MessageTimeout.SecondsSpecified = true;
        }

        public void SetMessageTimeoutThrow(bool throwOnTimeout)
        {
            this.MessageTimeout.ThrowExceptionOnTimeout = throwOnTimeout;
            this.MessageTimeout.ThrowExceptionOnTimeoutSpecified = true;
        }
    }

    public partial class SendPipelineServiceResolutionBatchTimeout
    {
        public TimeSpan ToTimeSpan()
        {
            return new TimeSpan(this.Days, this.Hours, this.Minutes, this.Seconds);
        }
    }

    public partial class SendPipelineServiceResolutionMessageTimeout
    {
        public TimeSpan ToTimeSpan()
        {
            return new TimeSpan(this.Days, this.Hours, this.Minutes, this.Seconds);
        }
    }
}
