using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.ESB.Resolver;
using System.Globalization;
using Microsoft.Practices.ESB.Exception.Management;
using System.Reflection;
using Microsoft.XLANGs.BaseTypes;
using Microsoft.Practices.ESB.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using System.Diagnostics;
using System.Xml;
using Microsoft.BizTalk.Message.Interop;
using Microsoft.BizTalk.Component.Interop;
using BTS;
using SOAP;
using Microsoft.Practices.ESB.GlobalPropertyContext;
using System.Runtime.InteropServices;

namespace ESB.Extensions.Resolution
{
    public static class ResolutionManager
    {
        // Fields
        private static Microsoft.Practices.ESB.Cache.Cache<IResolutionProvider> _ResolverProviderCache;
        private const string AdapterSeparator = "://";
        public const string CacheTimeout = "CacheTimeOut";
        private static string ConfigSeparator = ";";
        private const string DefaultWCFAdapter = "WCF-BasicHttp";
        public const string EndpointConfig = "EndpointConfig";
        private static string EscapeSeparator = @"\";
        public const string ForwardSeparator = "://";
        public const string JaxRpcResponse = "JaxRpcResponse";
        public const string MesOneWay = "one-way";
        public const string MessageExchangePattern = "MessageExchangePattern";
        public const string MesTwoWay = "two-way";
        public const string MonikerSeparator = @":\";
        private static string PropValueEscapeSeparator = "=~";
        public const string PropValueSeparator = "=";
        public const string ResolverPrefix = "Resolver";
        private const string ResolverProviderCacheMgr = "Resolver Providers Cache Manager";
        private static object syncObject = new object();
        public const string TargetNamespace = "TargetNamespace";
        public const string TransformType = "TransformType";
        public const string TransportLocation = "TransportLocation";
        public const string TransportType = "TransportType";
        public const string WSAction = "Action";

        // Methods
        private static void ConfigHelper_OnEsbConfigurationChanged(object sender, ConfigurationChangedEventArgs e)
        {
            lock (syncObject)
            {
                ResolutionProviderCache.Flush();
            }
        }

        public static object GetConfigValue(ResolutionDictionary resolution, bool required, string key)
        {
            string str;
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }
            if (resolution == null)
            {
                throw new ArgumentNullException("facts");
            }
            key = key.Trim().ToUpper(CultureInfo.CurrentCulture);
            try
            {
                if (resolution.ContainsKey(key))
                {
                    return resolution.GetValue(key);
                }
                if (required)
                {
                    throw new GetConfigValueException(0x2f5d5, new object[] { key });
                }
                str = string.Empty;
            }
            catch (Exception exception)
            {
                EventLogger.Write(MethodBase.GetCurrentMethod(), exception);
                throw;
            }
            return str;
        }

        public static ResolutionDictionary GetFacts(string config, string resolver)
        {
            ResolutionDictionary resolution = new ResolutionDictionary();
            config = config.Trim();
            resolver = resolver.Trim();
            string str = null;
            string str2 = null;
            try
            {
                if (!config.StartsWith(resolver, StringComparison.CurrentCultureIgnoreCase))
                {
                    throw new GetFactsException(0x2f5d4, new object[] { resolver });
                }
                config = config.Remove(0, resolver.Length);
                if (config.StartsWith(EscapeSeparator, StringComparison.CurrentCultureIgnoreCase))
                {
                    config = config.Remove(0, EscapeSeparator.Length);
                }
                if (config.EndsWith(ConfigSeparator, StringComparison.CurrentCultureIgnoreCase))
                {
                    config = config.Remove(config.Length - 1, 1);
                }
                string[] strArray = config.Split(ConfigSeparator.ToCharArray());
                if (strArray == null)
                {
                    throw new GetFactsException(0x2f5d3, new object[] { config });
                }
                foreach (string str3 in strArray)
                {
                    str = str3;
                    str2 = "=";
                    if (str3.Contains(PropValueEscapeSeparator))
                    {
                        str = str3.Replace(PropValueEscapeSeparator, "‡");
                        str2 = "‡";
                    }
                    int length = str.IndexOf(str2, 0, StringComparison.CurrentCultureIgnoreCase);
                    if (length < 1)
                    {
                        throw new GetFactsException(0x2f5d2, new object[] { str2, str3, config });
                    }
                    string str4 = str.Substring(0, length);
                    string str5 = string.Empty;
                    if (str.Length > length)
                    {
                        str5 = str.Substring(length + "=".Length).Trim();
                    }
                    if (!string.IsNullOrEmpty(str5))
                    {
                        resolution.SetValue(str4.Trim().ToUpper(CultureInfo.CurrentCulture), str5.Trim());
                    }
                }
                if (strArray != null)
                {
                    strArray = null;
                }
                if (resolution.Count < 1)
                {
                    throw new GetFactsException(0x2f5d1, new object[] { config });
                }
                return resolution;
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

        private static IResolutionProvider GetResolver(ResolverInfo info)
        {
            IResolutionProvider provider;
            string key = info.Resolver.Replace(@":\", "").ToUpper(CultureInfo.CurrentCulture).Trim();
            try
            {
                lock (syncObject)
                {
                    provider = ResolutionProviderCache.Get(key) as IResolutionProvider;
                }
                if (provider != null)
                {
                    EventLogger.Write("               ResolutionManager.GetResolver() : Resolver ** FOUND IN EXISTING CACHE ** = " + key);
                    return provider;
                }
                EventLogger.Write("               ResolutionManager.GetResolver() : Resolver NOT FOUND IN EXISTING CACHE = " + key);
                provider = ResolverFactory.Create(key);
                if (provider == null)
                {
                    throw new GetResolverException(0x2f5d6, new object[] { key });
                }
                lock (syncObject)
                {
                    ResolutionProviderCache.Add(key.ToUpper(CultureInfo.CurrentCulture), provider, new TimeSpan(0, 0, ResolverConfigHelper.CacheTimeout));
                }
                EventLogger.Write("               ResolutionManager.GetResolver() : Resolver **ADDED TO CACHE ** = " + key);
                return provider;
            }
            catch (Exception exception)
            {
                EventLogger.Write(MethodBase.GetCurrentMethod(), exception);
                throw;
            }
        }

        public static ResolverInfo GetResolverInfo(string config)
        {
            if (string.IsNullOrEmpty(config))
            {
                throw new ArgumentNullException("config");
            }
            ResolverInfo info = new ResolverInfo();
            info.Success = false;
            try
            {
                if (!string.IsNullOrEmpty(config))
                {
                    info.Config = config;
                    int num = info.Config.IndexOf(@":\", 0, StringComparison.CurrentCultureIgnoreCase);
                    if (num > 0)
                    {
                        info.Resolver = info.Config.Substring(0, num + @":\".Length);
                        info.Success = true;
                    }
                }
                info.Config = info.Config.Replace("&amp", "&");
            }
            catch (Exception exception)
            {
                EventLogger.Write(MethodBase.GetCurrentMethod(), exception);
                throw;
            }
            return info;
        }

        public static bool IsBtsCategoryKey(string key)
        {
            bool flag2;
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }
            bool flag = true;
            try
            {
                switch (key)
                {
                    case "EndpointConfig":
                    case "Action":
                    case "MessageExchangePattern":
                    case "TargetNamespace":
                    case "TransportLocation":
                    case "TransportType":
                    case "JaxRpcResponse":
                    case "TransformType":
                    case "CacheTimeOut":
                        break;

                    default:
                        flag = false;
                        break;
                }
                flag2 = flag;
            }
            catch (Exception exception)
            {
                EventLogger.Write(MethodBase.GetCurrentMethod(), exception);
                throw;
            }
            return flag2;
        }

        public static ResolutionDictionary Resolve(string config)
        {
            ResolutionDictionary resolution = null;
            try
            {
                if (string.IsNullOrEmpty(config))
                {
                    throw new ArgumentNullException("config");
                }
                config = config.Trim();
                ResolverInfo resolverInfo = GetResolverInfo(config);
                if (!resolverInfo.Success)
                {
                    throw new ResolveException(0x2f764, new object[] { config });
                }
                resolution = Resolve(resolverInfo, string.Empty);
            }
            catch (Exception exception)
            {
                EventLogger.LogMessage(EventFormatter.FormatEvent(MethodBase.GetCurrentMethod(), exception), EventLogEntryType.Error, 0x17ac);
                throw;
            }
            return resolution;
        }

        public static ResolutionDictionary Resolve(ResolverInfo info, XLANGMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException("message");
            }
            IResolutionProvider resolver = null;
            try
            {
                resolver = GetResolver(info);
                if (resolver == null)
                {
                    throw new GetResolverTypeException(0x2f5d0);
                }
                return resolver.Resolve(info, message);
            }
            catch (Exception exception)
            {
                EventLogger.LogMessage(EventFormatter.FormatEvent(MethodBase.GetCurrentMethod(), exception), EventLogEntryType.Error, 0x17ac);
                throw;
            }
        }

        public static ResolutionDictionary Resolve(ResolverInfo info, string message)
        {
            XmlDocument document = new XmlDocument();
            try
            {
                IResolutionProvider resolver = GetResolver(info);
                if (resolver == null)
                {
                    throw new GetResolverTypeException(0x2f5d0);
                }

                ResolutionDictionary resolution;
                if (string.IsNullOrEmpty(message))
                {
                    resolution = resolver.Resolve(info.Config, info.Resolver, null);
                }
                else
                {
                    document.LoadXml(message);
                    resolution = resolver.Resolve(info.Config, info.Resolver, document);
                }
                return resolution;
            }
            catch (Exception exception)
            {
                EventLogger.LogMessage(EventFormatter.FormatEvent(MethodBase.GetCurrentMethod(), exception), EventLogEntryType.Error, 0x17ac);
                throw;
            }
        }

        public static ResolutionDictionary Resolve(XLANGMessage message, string config)
        {
            try
            {
                if (string.IsNullOrEmpty(config))
                {
                    throw new ArgumentNullException("config");
                }
                if (message == null)
                {
                    throw new ArgumentNullException("message");
                }
                config = config.Trim();
                ResolverInfo resolverInfo = GetResolverInfo(config);
                if (!resolverInfo.Success)
                {
                    throw new ResolveException(0x2f764, new object[] { config });
                }
                return Resolve(resolverInfo, message);
            }
            catch (Exception exception)
            {
                EventLogger.LogMessage(EventFormatter.FormatEvent(MethodBase.GetCurrentMethod(), exception), EventLogEntryType.Error, 0x17ac);
                throw;
            }
        }

        public static ResolutionDictionary Resolve(string config, IBaseMessage message, IPipelineContext pipelineContext)
        {
            ResolverInfo ri = ResolutionManager.GetResolverInfo(config);
            return ResolutionManager.Resolve(ri, message, pipelineContext);
        }

        public static ResolutionDictionary Resolve(ResolverInfo info, IBaseMessage message, IPipelineContext pipelineContext)
        {
            if (message == null)
            {
                throw new ArgumentNullException("message");
            }
            if (pipelineContext == null)
            {
                throw new ArgumentNullException("pipelineContext");
            }
            try
            {
                IResolutionProvider resolver = GetResolver(info);
                if (resolver == null)
                {
                    throw new GetResolverTypeException(0x2f5d0);
                }
                return resolver.Resolve(info.Config, info.Resolver, message, pipelineContext);
            }
            catch (Exception exception)
            {
                EventLogger.LogMessage(EventFormatter.FormatEvent(MethodBase.GetCurrentMethod(), exception), EventLogEntryType.Error, 0x17ac);
                throw;
            }
        }

        public static void SetContext(ResolutionDictionary resolution, XLANGMessage message, Type[] propertyTypes)
        {
            if (message == null)
            {
                throw new ArgumentNullException("message");
            }
            if (resolution == null)
            {
                throw new ArgumentNullException("resolution");
            }
            try
            {
                resolution.SetValue("Action", GetMsgProperty(message, typeof(Action)));
                resolution.SetValue("DocumentSpecStrongName", GetMsgProperty(message, typeof(SchemaStrongName)));
                resolution.SetValue("MessageType", GetMsgProperty(message, typeof(MessageType)));
                object dssnf = resolution.GetValue("DocumentSpecStrongName");
                if (null != dssnf)
                {
                    string dssnfs = (string)dssnf;
                    if (!string.IsNullOrEmpty(dssnfs))
                    {
                        int index = dssnfs.IndexOf(",", StringComparison.CurrentCultureIgnoreCase);
                        if ((index > 0) && (index < dssnfs.Length))
                        {
                            resolution.SetValue("DocumentSpecName", dssnfs.Substring(0, index));
                        }
                        else
                        {
                            resolution.SetValue("DocumentSpecName", dssnfs);
                        }
                    }
                }
                resolution.SetValue("EpmRRCorrelationToken", GetMsgProperty(message, typeof(EpmRRCorrelationToken)));
                resolution.SetValue("InboundTransportLocation", GetMsgProperty(message, typeof(InboundTransportLocation)));
                resolution.SetValue("InboundTransportType", GetMsgProperty(message, typeof(InboundTransportType)));
                resolution.SetValue("InterchangeId", GetMsgProperty(message, typeof(InterchangeID)));
                resolution.SetValue("IsRequestResponse", GetMsgProperty(message, typeof(IsRequestResponse)));
                resolution.SetValue("ReceivePortName", GetMsgProperty(message, typeof(ReceivePortName)));
                resolution.SetValue("MethodName", GetMsgProperty(message, typeof(MethodName)));
                resolution.SetValue("WindowUser", GetMsgProperty(message, typeof(WindowsUser)));

                foreach (Type t in propertyTypes)
                {
                    object propValue = GetMsgProperty(message, t);
                    resolution.SetValue(t.FullName, propValue);
                }
            }
            catch (Exception exception)
            {
                EventLogger.Write(MethodBase.GetCurrentMethod(), exception);
                throw;
            }
        }

        public static void SetContext(ResolutionDictionary resolution, IBaseMessage message, IPipelineContext pipelineContext, Type[] propertyTypes)
        {
            if (pipelineContext == null)
            {
                throw new ArgumentNullException("pipelineContext");
            }
            if (message == null)
            {
                throw new ArgumentNullException("message");
            }
            if (resolution == null)
            {
                throw new ArgumentNullException("resolution");
            }
            try
            {
                resolution.SetValue("Action", message.Context.Read(WcfProperties.Action.Name, WcfProperties.Action.Namespace));
                resolution.SetValue("DocumentSpecStrongName", message.Context.Read(BtsProperties.SchemaStrongName.Name, BtsProperties.SchemaStrongName.Namespace));
                //EventLogger.Write("Document strong name is {0} after first context read.", new object[] { resolution.DocumentSpecStrongNameField });
                resolution.SetValue("MessageType", message.Context.Read(BtsProperties.MessageType.Name, BtsProperties.MessageType.Namespace));
                //EventLogger.Write("Document message type is {0} after first context read.", new object[] { resolution.MessageType });
                object dssnf = resolution.GetValue("DocumentSpecStrongName");
                if (null != dssnf)
                {
                    string dssnfs = (string)dssnf;
                    if (!string.IsNullOrEmpty(dssnfs))
                    {
                        int index = dssnfs.IndexOf(",", StringComparison.CurrentCultureIgnoreCase);
                        string dsnf = dssnfs;
                        if ((index > 0) && (index < dssnfs.Length))
                        {
                            dsnf = dssnfs.Substring(0, index);
                        }
                        resolution.SetValue("DocumentSpecName", dsnf);

                        try
                        {
                            IDocumentSpec documentSpecByName = pipelineContext.GetDocumentSpecByName(dssnfs);
                            if (documentSpecByName != null)
                            {
                                if (string.IsNullOrEmpty(dsnf))
                                {
                                    resolution.SetValue("DocumentSpecName", documentSpecByName.DocSpecName);
                                }
                                object mto = resolution.GetValue("MessageType");
                                if (null != mto)
                                {
                                    string mts = (string)mto;
                                    if (string.IsNullOrEmpty(mts))
                                    {
                                        resolution.SetValue("MessageType", documentSpecByName.DocType);
                                    }
                                }
                            }
                        }
                        catch (DocumentSpecException)
                        {
                        }
                        catch (COMException)
                        {
                        }
                    }
                }
                //EventLogger.Write("Document name is {0} after check.", new object[] { resolution.DocumentSpecNameField });
                //EventLogger.Write("Document string name is {0} after check.", new object[] { resolution.DocumentSpecStrongNameField });
                //EventLogger.Write("Document message type is {0} after check.", new object[] { resolution.MessageType });
                resolution.SetValue("EpmRRCorrelationToken", message.Context.Read(BtsProperties.EpmRRCorrelationToken.Name, BtsProperties.EpmRRCorrelationToken.Namespace));
                resolution.SetValue("InboundTransportLocation", message.Context.Read(BtsProperties.InboundTransportLocation.Name, BtsProperties.InboundTransportLocation.Namespace));
                resolution.SetValue("InboundTransportType", message.Context.Read(BtsProperties.InboundTransportType.Name, BtsProperties.InboundTransportType.Namespace));
                resolution.SetValue("InterchangeId", message.Context.Read(BtsProperties.InterchangeID.Name, BtsProperties.InterchangeID.Namespace));
                resolution.SetValue("IsRequestResponse", message.Context.Read(BtsProperties.IsRequestResponse.Name, BtsProperties.IsRequestResponse.Namespace));
                resolution.SetValue("ReceiveLocationName", message.Context.Read("ReceiveLocationName", BtsProperties.MessageType.Namespace));
                resolution.SetValue("ReceivePortName", message.Context.Read(BtsProperties.ReceivePortName.Name, BtsProperties.ReceivePortName.Namespace));
                resolution.SetValue("MethodName", message.Context.Read(SoapProperties.MethodName.Name, SoapProperties.MethodName.Namespace));
                resolution.SetValue("WindowUser", message.Context.Read(BtsProperties.WindowsUser.Name, BtsProperties.WindowsUser.Namespace));

                foreach (Type t in propertyTypes)
                {
                    PropertyBase pb = Activator.CreateInstance(t) as PropertyBase;
                    object propValue = message.Context.Read(pb.Name.Name, pb.Name.Namespace);
                    resolution.SetValue(t.FullName, propValue);
                }

            }
            catch (Exception exception)
            {
                EventLogger.Write(MethodBase.GetCurrentMethod(), exception);
                throw;
            }
        }

        // Properties
        public static Microsoft.Practices.ESB.Cache.Cache<IResolutionProvider> ResolutionProviderCache
        {
            get
            {
                if (_ResolverProviderCache == null)
                {
                    _ResolverProviderCache = new Microsoft.Practices.ESB.Cache.Cache<IResolutionProvider>(ResolverConfigHelper.CacheManagerName);
                    ConfigHelper.OnEsbConfigurationChanged += new EventHandler<ConfigurationChangedEventArgs>(ResolutionManager.ConfigHelper_OnEsbConfigurationChanged);
                }
                return _ResolverProviderCache;
            }
        }
    }
}
