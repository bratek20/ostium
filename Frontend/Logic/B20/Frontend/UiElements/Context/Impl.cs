using B20.Architecture.Contexts.Api;

namespace B20.Frontend.UiElements.Context
{
    public class UiElementsImpl: ContextModule
    {
        public void Apply(ContextBuilder builder)
        {
            builder
                .SetClass<InputField>(InjectionMode.Prototype);
        }
    }
}