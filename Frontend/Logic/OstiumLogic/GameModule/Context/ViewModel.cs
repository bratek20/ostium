using B20.Architecture.Contexts.Api;

namespace Ostium.Logic.GameModule.Context
{
    public class GameModuleViewModel: ContextModule
    {
        public void Apply(ContextBuilder builder)
        {
            builder
                .SetClass<GameVM>()
                .SetClass<TableVM>()
                .SetClass<HandVm>()
                .SetClass<CreateCardListVm>()
                .SetClass<OptionalCreatureCardVm>()
                .SetClass<RowVM>()
                .SetClass<CreatureCardVm>();
        }
    }
}