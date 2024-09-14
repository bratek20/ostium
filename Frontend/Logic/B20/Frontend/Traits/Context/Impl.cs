using B20.Architecture.Contexts.Api;
using B20.Frontend.Element;
using B20.Frontend.Traits.Impl;

namespace B20.Frontend.Traits.Context
{
    public class TraitsImpl: ContextModule
    {
        public void Apply(ContextBuilder builder)
        {
            builder
                .SetImpl<TraitFactory, TraitFactoryLogic>()
                .SetClass<WithRect>()
                .SetClass<Clickable>();
        }
    }
}