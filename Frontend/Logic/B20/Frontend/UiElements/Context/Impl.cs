using B20.Architecture.Contexts.Api;
using B20.Frontend.UiElements;

namespace B20.Frontend.UiElements.Context
{
    public class UiElementsImpl: ContextModule
    {
        public void Apply(ContextBuilder builder)
        {
            builder
                .SetClass<InputField>(InjectionMode.Prototype)
                .SetClass<Button>(InjectionMode.Prototype)
                .SetClass<Label>(InjectionMode.Prototype)
                .SetClass<OptionalLabel>(InjectionMode.Prototype)
                .SetClass<BoolSwitch>(InjectionMode.Prototype)
                .SetClass<EnumSwitch>(InjectionMode.Prototype);
        }
    }
}