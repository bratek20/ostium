using B20.Architecture.Contexts.Api;
using B20.Frontend.Windows.Api;
using SingleGame.Api;
using SingleGame.ViewModel;

namespace SingleGame.Context
{
    public class SingleGameViewModel: ContextModule
    {
        public void Apply(ContextBuilder builder)
        {
            builder
                .AddImpl<Window, GameWindow>()
                .SetClass<GameVm>(InjectionMode.Prototype)
                .SetClass<TableVm>(InjectionMode.Prototype)
                .SetClass<PlayerSideVm>(InjectionMode.Prototype)
                .SetClass<HandVm>(InjectionMode.Prototype)
                .SetClass<CreateCardListVm>(InjectionMode.Prototype)
                .SetClass<OptionalCreatureCardVm>(InjectionMode.Prototype)
                .SetClass<RowVm>(InjectionMode.Prototype)
                .SetClass<CreatureCardVm>(InjectionMode.Prototype);
        }
    }
}