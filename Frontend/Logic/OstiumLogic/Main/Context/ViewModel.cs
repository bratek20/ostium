using B20.Architecture.Contexts.Api;
using B20.Frontend.Windows.Api;
using SingleGame.ViewModel;
using Main.ViewModel;

namespace Ostium.Logic.MainWindowModule.Context
{
    public class MainViewModel: ContextModule
    {
        public void Apply(ContextBuilder builder)
        {
            builder
                .AddImpl<Window, MainWindow>()
                .SetClass<PlayButton>(InjectionMode.Prototype);
        }
    }
}