using B20.Architecture.Contexts.Api;
using B20.Logic;

namespace B20.Frontend.UiElements.Context
{
    public class UiElementsImpl: ContextModule
    {
        public void Apply(ContextBuilder builder)
        {
            builder
                .SetClass<InputField>(InjectionMode.Prototype)
                .SetClass<Button>(InjectionMode.Prototype);
        }
    }
}