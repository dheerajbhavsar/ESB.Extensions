using System;
using System.ComponentModel;
using Microsoft.Practices.Services.ItineraryDsl;
using Microsoft.Practices.Modeling.ExtensionProvider.Metadata;
using Microsoft.Practices.Modeling.ExtensionProvider.Extension;

namespace ESB.Extensions.Itinerary.Extenders
{
    public class BREResolverExtender : BreResolverExtensionProvider
    {
        [Category("Extender Settings"), Description("Specifies the context properties to include in the ResolutionDictionary. Type FullNames, seperated by pipe token ('|'), like the DocSpecNames for XmlDisassembler."), DisplayName("PropertyTypes"), ReadOnly(false), Browsable(true)]
        public string PropertyTypes { get; set; }
    }

    [ResolverExtensionProvider, ExtensionProvider("73DAD74B-04A3-42c4-8D3E-DCD5F46589EA", "BRE2", "BRE2 Resolver Extension", typeof(ItineraryDslDomainModel))]
    public class BreResolverExtensionProvider : ExtensionProviderBase
    {
        // Methods
        public BreResolverExtensionProvider()
            : base(new Type[] { typeof(BREResolverExtender) })
        {
        }
    }
}
