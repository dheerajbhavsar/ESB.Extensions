using System.Collections.Generic;
using System.Xml;

using Microsoft.Practices.ESB.Resolver;
using Microsoft.BizTalk.Message.Interop;
using Microsoft.BizTalk.Component.Interop;
using Microsoft.XLANGs.BaseTypes;

namespace ESB.Extensions.Resolution
{
    public interface IResolutionProvider
    {
        ResolutionDictionary Resolve(ResolverInfo resolverInfo, XLANGMessage message);
        ResolutionDictionary Resolve(string config, string resolver, XmlDocument message);
        ResolutionDictionary Resolve(string config, string resolver, IBaseMessage message, IPipelineContext pipelineContext);
    }
}
