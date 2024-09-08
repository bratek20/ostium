using B20.Architecture.Contexts.Api;

namespace Ostium.Logic.GameModule.Context
{
    public class GameModuleViewModel: ContextModule
    {
        public void Apply(ContextBuilder builder)
        {
            builder
                .SetClass<GameVM>(InjectionMode.Prototype)
                .SetClass<TableVM>(InjectionMode.Prototype)
                .SetClass<HandVm>(InjectionMode.Prototype)
                .SetClass<CreateCardListVm>(InjectionMode.Prototype)
                .SetClass<OptionalCreatureCardVm>(InjectionMode.Prototype)
                .SetClass<RowVM>(InjectionMode.Prototype)
                .SetClass<CreatureCardVm>(InjectionMode.Prototype);
        }
    }
}