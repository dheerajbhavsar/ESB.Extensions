using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.ESB.Configuration;
using Microsoft.Practices.ESB.Resolver;
using Microsoft.Practices.ESB.Exception.Management;

namespace ESB.Extensions.Resolution
{
internal class ResolverFactory
{
    // Fields
    private const string SectionName = "esb";

    // Methods
    internal static IResolutionProvider Create(string key)
    {
        if (key == null)
        {
            throw new ArgumentNullException("key");
        }
        IResolutionProvider provider = null;
        ESBConfigurationSection section = null;
        string typeName = null;
        Type type = null;
        try
        {
            Microsoft.Practices.ESB.Configuration.Resolver resolver = ResolverConfigHelper.FindResolverByKey(key);
            typeName = resolver.TypeName;
            if (typeName == null)
            {
                throw new GetResolverException(0x2f5d9, new object[] { "esb", key });
            }
            type = Type.GetType(typeName, true, true);
            if (type.GetInterface(typeof(IResolutionProvider).Name) == null)
            {
                throw new GetResolverException(0x2f5d7, new object[] { type.FullName });
            }
            if (resolver.ResolverConfig.Count > 0)
            {
                provider = Activator.CreateInstance(type, new object[] { resolver }) as IResolutionProvider;
            }
            else
            {
                provider = (IResolutionProvider) Activator.CreateInstance(type);
            }
            if (provider == null)
            {
                throw new GetResolverException(0x2f5d7, new object[] { type.FullName });
            }
            EventLogger.Write("               ResolutionManager.GetResolver() : Resolver **ADDED TO CACHE ** = " + key);
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            if (section != null)
            {
                section = null;
            }
            if (typeName != null)
            {
                typeName = null;
            }
        }
        return provider;
    }
}
}
