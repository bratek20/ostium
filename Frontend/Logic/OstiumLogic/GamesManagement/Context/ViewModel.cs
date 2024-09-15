using B20.Architecture.Contexts.Api;
using B20.Frontend.Windows.Api;
using SingleGame.ViewModel;
using Main.ViewModel;

namespace GamesManagement.Context
{
    public class GamesManagementViewModel: ContextModule
    {
        public void Apply(ContextBuilder builder)
        {
            builder
                .AddImpl<Window, GamesManagementWindow>();
        }
    }
}