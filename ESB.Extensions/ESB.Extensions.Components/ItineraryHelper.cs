using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.XLANGs.BaseTypes;

using Microsoft.Practices.ESB.Itinerary;
using Microsoft.Practices.ESB.Itinerary.OM.V1;
using Microsoft.Practices.ESB.Exception.Management;
using System.Reflection;
using System.Xml;
using Microsoft.Practices.ESB.GlobalPropertyContext;
using Microsoft.Practices.ESB.Adapter;
using Microsoft.Practices.ESB.Resolver;
using BTS;
using Microsoft.Practices.ESB.Itinerary.Schemas;

namespace ESB.Extensions.Components
{
    public static class BtsProperties1
    {
        static BtsProperties1()
        {
            _receiveInstanceID = new XmlQualifiedName("ReceiveInstanceID", BtsProperties.InterchangeID.Namespace);
        }

        private static XmlQualifiedName _receiveInstanceID;
        public static XmlQualifiedName ReceiveInstanceID
        {
            get { return _receiveInstanceID; }
        }
    }

    //[Serializable, PropertyType("ReceiveInstanceID", "http://schemas.microsoft.com/BizTalk/2003/system-properties", "string", "System.String"), Guid("f3cba60e-f53c-41c5-93a7-07132dc1f678"), IsSensitiveProperty(false), PropertyGuid("f3cba60e-f53c-41c5-93a7-07132dc1f678")]
    public sealed class ReceiveInstanceID : MessageContextPropertyBase
    {
        // Fields
        private static XmlQualifiedName _QName = BtsProperties1.ReceiveInstanceID;

        // Properties
        public override XmlQualifiedName Name
        {
            get
            {
                return _QName;
            }
        }

        private static string PropertyValueType
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        public override Type Type
        {
            get
            {
                return typeof(string);
            }
        }
    }

    public static class ItineraryHelper
    {
        public static void Initialize(string itineraryString, XLANGMessage message)
        {
            ItineraryV1 itinerary = ItineraryOMFactory.Create(itineraryString) as ItineraryV1;
            itinerary.Write(message);
            IItineraryStep step = itinerary.GetItineraryStep(message);

            bool isRequestResponse = false;
            if (((step.State == State.Pending) && itinerary.IsInvocationService(step)) && (step.ResolverCollection.Count > 0))
            {
                AdapterMgr.SetEndpoint(ResolverMgr.Resolve(message, step.ResolverCollection[0]), message);
                isRequestResponse = itinerary.ItineraryData.ServiceInstance.isRequestResponse;
            }
            else
            {
                message.SetPropertyValue(typeof(OutboundTransportLocation), "");
                message.SetPropertyValue(typeof(OutboundTransportType), "FILE");
            }
            SetCorrelationProperties(itinerary, message, isRequestResponse);
            SetServiceProperties(itinerary, message, step.State);
        }

        private static void SetCorrelationProperties(ItineraryV1 itinerary, XLANGMessage message, bool setEpmRR)
        {
            string str = "Unknown:0";
            string str2 = "0";
            string str3 = "0";
            int num = 0;
            bool flag = false;
            string propertyValue = message.GetPropertyValue(typeof(CorrelationToken)) as string;
            string str5 = message.GetPropertyValue(typeof(ReqRespTransmitPipelineID)) as string;
            string str6 = message.GetPropertyValue(typeof(EpmRRCorrelationToken)) as string;
            object obj2 = message.GetPropertyValue(typeof(BTS.IsRequestResponse));
            message.GetPropertyValue(typeof(RouteDirectToTP));
            if (setEpmRR)
            {
                string[] strArray = itinerary.ItineraryData.BizTalkSegment.epmRRCorrelationToken.Split("|".ToCharArray());
                if (strArray == null)
                {
                    throw new ItinerarySetCorrelationException(0x71868);
                }
                if (strArray.GetUpperBound(0) != 2)
                {
                    throw new ItinerarySetCorrelationException(0x71868);
                }
                str = strArray[0];
                str2 = strArray[1];
                str3 = strArray[2];
                num = 1;
                flag = true;
            }
            if (string.IsNullOrEmpty(str6) || (str6 == "Unknown:0"))
            {
                message.SetPropertyValue(typeof(EpmRRCorrelationToken), str);
            }
            if (obj2 == null)
            {
                message.SetPropertyValue(typeof(Microsoft.Practices.ESB.Itinerary.Schemas.IsRequestResponse), num);
            }
            if (string.IsNullOrEmpty(propertyValue) || (propertyValue == "0"))
            {
                message.SetPropertyValue(typeof(CorrelationToken), str2);
            }
            if (string.IsNullOrEmpty(str5) || (str5 == "0"))
            {
                message.SetPropertyValue(typeof(ReqRespTransmitPipelineID), str3);
            }
            message.SetPropertyValue(typeof(RouteDirectToTP), flag);
        }

        private static void SetServiceProperties(ItineraryV1 itinerary, XLANGMessage message, string state)
        {
            message.SetPropertyValue(typeof(ServiceName), itinerary.ItineraryData.ServiceInstance.name.Trim());
            message.SetPropertyValue(typeof(ServiceType), itinerary.ItineraryData.ServiceInstance.type.Trim());
            message.SetPropertyValue(typeof(ServiceState), state);
            message.SetPropertyValue(typeof(Microsoft.Practices.ESB.Itinerary.Schemas.IsRequestResponse), itinerary.ItineraryData.ServiceInstance.isRequestResponse);
        }

        public static void Initialize(this ItineraryV1 itinerary, XLANGMessage msg, string uuid, ItineraryV1 originalItinerary)
        {
            try
            {
                if (string.IsNullOrEmpty(itinerary.ItineraryData.uuid))
                {
                    itinerary.ItineraryData.uuid = uuid;
                }
                itinerary.ItineraryData.beginTime = DateTime.Now.ToUniversalTime().ToString("o");
                itinerary.ItineraryData.completeTime = "";
                itinerary.ItineraryData.servicecount = itinerary.ItineraryData.Services.Length;
                if (itinerary.ItineraryData.BizTalkSegment == null)
                {
                    itinerary.ItineraryData.BizTalkSegment = new ItineraryBizTalkSegment();
                }
                string str = msg.GetPropertyValue(typeof(BTS.CorrelationToken)) as string;
                string str2 = GetMsgProperty(msg, typeof(BTS.ReqRespTransmitPipelineID)) as string;
                string str3 = GetMsgProperty(msg, typeof(BTS.InterchangeID)) as string;
                string str4 = GetMsgProperty(msg, typeof(ReceiveInstanceID)) as string;
                string str5 = GetMsgProperty(msg, typeof(BTS.MessageID)) as string;
                string str6 = GetMsgProperty(msg, typeof(BTS.EpmRRCorrelationToken)) as string;
                str6 = str6 + "|" + str + "|" + str2;
                itinerary.ItineraryData.BizTalkSegment.epmRRCorrelationToken = str6 ?? "";
                itinerary.ItineraryData.BizTalkSegment.receiveInstanceId = str4 ?? "";
                itinerary.ItineraryData.BizTalkSegment.messageId = str5 ?? "";
                itinerary.ItineraryData.BizTalkSegment.interchangeId = str3 ?? "";
                if (itinerary.ItineraryData.ServiceInstance == null)
                {
                    itinerary.ItineraryData.ServiceInstance = new ItineraryServiceInstance();
                    ItineraryServicesService service = itinerary.ItineraryData.Services.Where<ItineraryServices>(delegate(ItineraryServices iservice)
                    {
                        return (iservice.Service.state != State.Complete);
                    }).Select<ItineraryServices, ItineraryServicesService>(delegate(ItineraryServices iservice)
                    {
                        return iservice.Service;
                    }).FirstOrDefault<ItineraryServicesService>();
                    if (service != null)
                    {
                        itinerary.ItineraryData.ServiceInstance.id = service.id;
                        itinerary.ItineraryData.ServiceInstance.name = service.name;
                        itinerary.ItineraryData.ServiceInstance.position = service.position;
                        itinerary.ItineraryData.ServiceInstance.positionSpecified = service.positionSpecified;
                        itinerary.ItineraryData.ServiceInstance.state = service.state;
                        itinerary.ItineraryData.ServiceInstance.stage = service.stage;
                        itinerary.ItineraryData.ServiceInstance.stageSpecified = service.stageSpecified;
                    }
                }
                object obj2 = GetMsgProperty(msg, typeof(BTS.IsRequestResponse));
                if (obj2 != null)
                {
                    bool flag = Convert.ToBoolean(obj2);
                    itinerary.ItineraryData.isRequestResponseSpecified = true;
                    itinerary.ItineraryData.isRequestResponse = flag;
                }
                else
                {
                    itinerary.ItineraryData.isRequestResponseSpecified = true;
                    itinerary.ItineraryData.isRequestResponse = false;
                }
            }
            catch (Exception exception)
            {
                EventLogger.Write(MethodBase.GetCurrentMethod(), exception);
                throw;
            }
        }

        private static object GetMsgProperty(XLANGMessage message, Type property)
        {
            object obj3;
            if (message == null)
            {
                throw new ArgumentNullException("message");
            }
            if (property == null)
            {
                throw new ArgumentNullException("property");
            }
            object propertyValue = null;
            try
            {
                propertyValue = message.GetPropertyValue(property);
                obj3 = propertyValue;
            }
            catch (InvalidPropertyTypeException exception)
            {
                EventLogger.Write(MethodBase.GetCurrentMethod(), exception);
                obj3 = propertyValue;
            }
            catch (Exception exception2)
            {
                EventLogger.Write(MethodBase.GetCurrentMethod(), exception2);
                throw;
            }
            finally
            {
                if (propertyValue != null)
                {
                    propertyValue = null;
                }
            }
            return obj3;
        }
    }
}
