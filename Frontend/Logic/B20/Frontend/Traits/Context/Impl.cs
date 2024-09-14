using B20.Architecture.Contexts.Api;
using B20.Frontend.UiElements;
using B20.Frontend.Traits.Impl;

namespace B20.Frontend.Traits.Context
{
    public class TraitsImpl: ContextModule
    {
        public void Apply(ContextBuilder builder)
        {
            builder
                .SetImpl<TraitFactory, TraitFactoryLogic>()
                .SetClass<WithRect>(InjectionMode.Prototype)
                .SetClass<Clickable>(InjectionMode.Prototype)
                .SetClass<WithPosition2d>(InjectionMode.Prototype)
                .SetClass<Draggable>(InjectionMode.Prototype);
        }
    }
}