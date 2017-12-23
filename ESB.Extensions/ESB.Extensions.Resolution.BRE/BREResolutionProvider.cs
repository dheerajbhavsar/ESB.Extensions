using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ESB.Extensions.Resolution;
using Microsoft.RuleEngine;
using Microsoft.Practices.ESB.Exception.Management;
using System.Globalization;
using System.Reflection;
using Microsoft.Practices.ESB.Resolver;
using System.Xml;
using System.IO;
using Microsoft.BizTalk.Message.Interop;
using Microsoft.BizTalk.Component.Interop;
using Microsoft.BizTalk.Streaming;
using Microsoft.XLANGs.BaseTypes;

namespace ESB.Extensions.Resolution.BRE
{
    public class BREResolutionProvider : IResolutionProvider
    {
        // Methods
        private BRE CreateResolverDescriptor(string config, string resolver)
        {
            BRE bre = null;
            if (config == null)
            {
                throw new ArgumentNullException("config");
            }
            if (resolver == null)
            {
                throw new ArgumentNullException("resolver");
            }
            Dictionary<string, string> facts = null;
            try
            {
                bre = new BRE();
                bre.useMsg = false;
                facts = ResolverMgr.GetFacts(config, resolver);
                bre.policy = ResolverMgr.GetConfigValue(facts, true, "policy");
                bre.version = ResolverMgr.GetConfigValue(facts, false, "version");
                string useMsgString = ResolverMgr.GetConfigValue(facts, false, "useMsg");
                if (!string.IsNullOrEmpty(useMsgString) && ((string.Compare(useMsgString, "true", true, CultureInfo.CurrentCulture) == 0) || (string.Compare(useMsgString, "false", true, CultureInfo.CurrentCulture) == 0)))
                {
                    bre.useMsg = Convert.ToBoolean(useMsgString, NumberFormatInfo.CurrentInfo);
                }
                string recognizeMessageFormatString = ResolverMgr.GetConfigValue(facts, false, "recognizeMessageFormat");
                if (!string.IsNullOrEmpty(recognizeMessageFormatString) && ((string.Compare(recognizeMessageFormatString, "true", true, CultureInfo.CurrentCulture) == 0) || (string.Compare(recognizeMessageFormatString, "false", true, CultureInfo.CurrentCulture) == 0)))
                {
                    bre.recognizeMessageFormat = Convert.ToBoolean(recognizeMessageFormatString, NumberFormatInfo.CurrentInfo);
                }
                bre.PropertyTypeNamesString = ResolverMgr.GetConfigValue(facts, false, "propertyTypes");
                bre.PropertyTypeNamesStringSpecified = true;

            }
            catch (Exception exception)
            {
                EventLogger.Write(MethodBase.GetCurrentMethod(), exception);
                throw;
            }
            finally
            {
                if (facts != null)
                {
                    facts.Clear();
                    facts = null;
                }
            }
            return bre;
        }

        ResolutionDictionary IResolutionProvider.Resolve(string config, string resolver, XmlDocument message)
        {
            ResolutionDictionary resolution = new ResolutionDictionary();
            if (string.IsNullOrEmpty(config))
            {
                throw new ArgumentNullException("config");
            }
            if (string.IsNullOrEmpty(resolver))
            {
                throw new ArgumentNullException("resolver");
            }
            try
            {
                BRE bre = this.CreateResolverDescriptor(config, resolver);
                if (bre.useMsg && bre.recognizeMessageFormat)
                {
                    throw new ResolveException("RecognizeMessageFormatNotSupportedXDoc");
                }
                ResolveRules(config, resolver, message, resolution, bre);
            }
            catch (Exception exception)
            {
                EventLogger.Write(MethodBase.GetCurrentMethod(), exception);
                throw;
            }
            return resolution;
        }

        ResolutionDictionary IResolutionProvider.Resolve(string config, string resolver, IBaseMessage message, IPipelineContext pipelineContext)
        {
            ResolutionDictionary resolution = new ResolutionDictionary();
            if (string.IsNullOrEmpty(config))
            {
                throw new ArgumentNullException("config");
            }
            if (string.IsNullOrEmpty(resolver))
            {
                throw new ArgumentNullException("resolver");
            }
            if (message == null)
            {
                throw new ArgumentNullException("message");
            }
            if (pipelineContext == null)
            {
                throw new ArgumentNullException("pipelineContext");
            }

            XmlDocument document = new XmlDocument();
            try
            {
                BRE bre = this.CreateResolverDescriptor(config, resolver);
                if (bre.useMsg && (message.BodyPart != null))
                {
                    Stream originalDataStream = message.BodyPart.GetOriginalDataStream();
                    if (!originalDataStream.CanSeek)
                    {
                        ReadOnlySeekableStream stream2 = new ReadOnlySeekableStream(originalDataStream);
                        message.BodyPart.Data = stream2;
                        pipelineContext.ResourceTracker.AddResource(stream2);
                        originalDataStream = stream2;
                    }
                    if (originalDataStream.Position != 0L)
                    {
                        originalDataStream.Position = 0L;
                    }
                    document.Load(originalDataStream);
                    originalDataStream.Position = 0L;
                }
                ResolutionManager.SetContext(resolution, message, pipelineContext, bre.PropertyTypes);
                ResolveRules(config, resolver, document, resolution, bre);
            }
            catch (Exception exception)
            {
                EventLogger.Write(MethodBase.GetCurrentMethod(), exception);
                throw;
            }
            return resolution;
        }

        ResolutionDictionary IResolutionProvider.Resolve(ResolverInfo resolverInfo, XLANGMessage message)
        {
            ResolutionDictionary resolution = new ResolutionDictionary();
            if (message == null)
            {
                throw new ArgumentNullException("message");
            }
            try
            {
                BRE bre = this.CreateResolverDescriptor(resolverInfo.Config, resolverInfo.Resolver);
                if (bre.useMsg && bre.recognizeMessageFormat)
                {
                    throw new ResolveException("RecognizeMessageFormatNotSupportedXLang");
                }
                ResolutionManager.SetContext(resolution, message, bre.PropertyTypes);

                XmlDocument document = null;
                if (bre.useMsg)
                {
                    document = (XmlDocument)message[0].RetrieveAs(typeof(XmlDocument));
                }
                ResolveRules(resolverInfo.Config, resolverInfo.Resolver, document, resolution, bre);
            }
            catch (Exception exception)
            {
                EventLogger.Write(MethodBase.GetCurrentMethod(), exception);
                throw;
            }
            return resolution;
        }

        private static void ResolveRules(string config, string resolver, XmlDocument message, ResolutionDictionary resolution, BRE bre)
        {
            int majorRevision = 0;
            int minorRevision = 0;
            Policy policy = null;
            string[] strArray = null;
            object[] facts = null;
            TypedXmlDocument document = null;
            string documentType = "Microsoft.Practices.ESB.ResolveProviderMessage";
            if (!resolver.Contains(@":\"))
            {
                resolver = resolver + @":\";
            }
            try
            {
                object dsnfo = resolution.GetValue("DocumentSpecName");
                EventLogger.Write("Resolution strong name is {0}.", new object[] { dsnfo });
                if (null != dsnfo)
                {
                    string dsnfs = (string)dsnfo;
                    if (!string.IsNullOrEmpty(dsnfs) && bre.recognizeMessageFormat)
                    {
                        int index = dsnfs.IndexOf(",", StringComparison.CurrentCultureIgnoreCase);
                        if ((index > 0) && (index < dsnfs.Length))
                        {
                            documentType = dsnfs.Substring(0, index);
                        }
                        else
                        {
                            documentType = dsnfs;
                        }
                    }
                }
                EventLogger.Write("DocType for typed xml document is {0}.", new object[] { documentType });
                if (!string.IsNullOrEmpty(bre.version))
                {
                    strArray = bre.version.Split(".".ToCharArray());
                    if (strArray != null)
                    {
                        majorRevision = Convert.ToInt16(strArray[0], NumberFormatInfo.CurrentInfo);
                        minorRevision = Convert.ToInt16(strArray[1], NumberFormatInfo.CurrentInfo);
                    }
                }
                if (bre.useMsg)
                {
                    facts = new object[2];
                    document = new TypedXmlDocument(documentType, message);
                    facts[0] = resolution;
                    facts[1] = document;
                }
                else
                {
                    facts = new object[] { resolution };
                }
                if (majorRevision > 0)
                {
                    policy = new Policy(bre.policy, majorRevision, minorRevision);
                }
                else
                {
                    policy = new Policy(bre.policy);
                }

                System.Diagnostics.Trace.WriteLine("ResolutionDictionary before BRE2 resolution:");
                System.Diagnostics.Trace.WriteLine(resolution.ToString());

                using (policy)
                {
                    policy.Execute(facts);
                }

                System.Diagnostics.Trace.WriteLine("ResolutionDictionary after BRE2 resolution:");
                System.Diagnostics.Trace.WriteLine(resolution.ToString());

                resolution = facts[0] as ResolutionDictionary;
                if (null == resolution)
                {
                    throw new ResolveException(0x2f6fc);
                }
            }
            catch (Exception exception)
            {
                EventLogger.Write(MethodBase.GetCurrentMethod(), exception);
                throw;
            }
            finally
            {
                if (null != facts)
                {
                    for (int i = 0; i < facts.Length; i++)
                    {
                        facts[i] = null;
                    }
                    facts = null;
                }
                document = null;
                //resolution = null;
                bre = null;
                strArray = null;
                policy = null;
            }
        }
    }
}
