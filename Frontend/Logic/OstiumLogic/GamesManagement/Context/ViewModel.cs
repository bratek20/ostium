using B20.Architecture.Contexts.Api;
using B20.Frontend.Windows.Api;
using GamesManagement.ViewModel;

namespace GamesManagement.Context
{
    public class GamesManagementViewModel: ContextModule
    {
        public void Apply(ContextBuilder builder)
        {
            builder
                .AddImpl<Window, GamesManagementWindow>()
                .SetClass<CreatedGameVmGroup>(InjectionMode.Prototype)
                .SetClass<CreatedGameVm>(InjectionMode.Prototype);
        }
    }
}