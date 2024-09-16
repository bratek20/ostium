using B20.Architecture.Contexts.Context;
using B20.Frontend.Traits.Context;
using B20.Frontend.UiElements.Context;
using B20.Frontend.Windows.Context;
using B20.Tests.Architecture.Logs.Context;
using B20.Tests.Frontend.Windows.Context;
using B20.Tests.Frontend.Windows.Fixtures;
using GamesManagement.Context;
using GamesManagement.ViewModel;
using Main.ViewModel;
using Ostium.Logic.MainWindowModule.Context;
using SingleGame.ViewModel;
using Xunit;

namespace GamesManagement.Tests
{
    public class GamesManagementViewModelTest
    {
        [Fact]
        public void TODO()
        {
            var c = ContextsFactory.CreateBuilder()
                .WithModules(
                    //TODO-REF below one should be somehow grouped
                    new LogsMocks(),
                    new TraitsImpl(),
                    new UiElementsImpl(),
                    
                    //reasonable mock
                    new WindowsMocks(),
                    
                    //tested module
                    new GamesManagementViewModel()
                ).Build();
            
            var window = c.Get<GamesManagementWindow>();
            var windowManagerMock = c.Get<WindowManagerMock>();
        }

    }
}