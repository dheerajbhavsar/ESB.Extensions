using System;
using System.Xml;
using System.Configuration;
using Microsoft.Practices.Unity;
using Microsoft.XLANGs.BaseTypes;
using Microsoft.Practices.ESB.Resolver;
using Microsoft.BizTalk.Message.Interop;
using Microsoft.BizTalk.Component.Interop;
using Microsoft.Practices.Unity.Configuration;
using Microsoft.Practices.ESB.Resolver.Container;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;

namespace ESB.Extensions.Resolution.UNITY
{
    public class UNITYResolutionProvider : IResolutionProvider
    {
        // Fields
        private IResolutionProvider _ResolutionProvider;
        private const string UnityContainerConfigToken = "unityContainerName";
        private const string UnitySectionConfigToken = "unitySectionName";

        // Methods
        private UNITYResolutionProvider()
        {
            throw new NotImplementedException();
        }

        public UNITYResolutionProvider(Microsoft.Practices.ESB.Configuration.Resolver resolverConfig)
        {
            this.InitializeProvider(resolverConfig);
        }

        private void InitializeProvider(Microsoft.Practices.ESB.Configuration.Resolver resolverSection)
        {
            if (resolverSection == null)
            {
                throw new ArgumentNullException("resolverSection");
            }

            string sectionConfig = ReadConfiguration(resolverSection, UnitySectionConfigToken);
            string containerConfig = ReadConfiguration(resolverSection, UnityContainerConfigToken);

            IUnityContainer uc = new UnityContainer();
            UnityConfigurationSection section = (UnityConfigurationSection)ConfigurationSourceFactory.Create().GetSection(sectionConfig);
            section.Containers[containerConfig].Configure(uc);

            this._ResolutionProvider = uc.Resolve<IResolutionProvider>();
            IResolveContainer rc = this._ResolutionProvider as IResolveContainer;
            if (rc == null)
            {
                throw new ApplicationException("UNITY Container error.");
            }
            rc.Initialize(uc);
        }

        private static string ReadConfiguration(Microsoft.Practices.ESB.Configuration.Resolver resolverSection, string key)
        {
            NameValueConfigurationElement element = resolverSection.ResolverConfig[key];
            if (element == null)
            {
                throw new ApplicationException("KeyMissingErrorMessage");
            }
            string str = element.Value;
            if (str == null)
            {
                throw new ApplicationException("ValueMissingErrorMessage");
            }
            return str;
        }

        public ResolutionDictionary Resolve(ResolverInfo resolverInfo, XLANGMessage message)
        {
            return this._ResolutionProvider.Resolve(resolverInfo, message);
        }

        public ResolutionDictionary Resolve(string config, string resolver, XmlDocument message)
        {
            return this._ResolutionProvider.Resolve(config, resolver, message);
        }

        public ResolutionDictionary Resolve(string config, string resolver, IBaseMessage message, IPipelineContext pipelineContext)
        {
            return this._ResolutionProvider.Resolve(config, resolver, message, pipelineContext);
        }
    }
}
