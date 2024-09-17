using B20.Architecture.Contexts.Api;
using B20.Frontend.Traits.Context;
using B20.Frontend.UiElements.Context;
using B20.Tests.Architecture.Logs.Context;

namespace B20.Tests.Frontend.TestHelpers
{
    public class ViewModelTesting: ContextModule
    {
        public void Apply(ContextBuilder builder)
        {
            builder.WithModules(
                new LogsMocks(),
                new TraitsImpl(),
                new UiElementsImpl()
            );
        }
    }
}