using B20.Architecture.Contexts.Context;
using B20.Frontend.Traits.Context;
using B20.Frontend.UiElements.Context;
using B20.Frontend.Windows.Api;
using B20.Tests.Architecture.Logs.Context;
using B20.Tests.ExtraAsserts;
using B20.Tests.Frontend.TestHelpers;
using B20.Tests.Frontend.Windows.Context;
using B20.Tests.Frontend.Windows.Fixtures;
using GamesManagement.ViewModel;
using Main.ViewModel;
using Ostium.Logic.MainWindowModule.Context;
using Xunit;

namespace Ostium.Logic.Tests.Main.Tests
{
    public class MainViewModelTest
    {
        [Fact]
        public void ShouldOpenManagementWindowPassingUsernameWhenPlayClicked()
        {
            var c = ContextsFactory.CreateBuilder()
                .WithModules(
                    new ViewModelTesting(),
                    
                    //tested module
                    new MainViewModel()
                ).Build();
            
            var mainWindow = c.Get<MainWindow>();
            var windowManagerMock = c.Get<WindowManagerMock>();

            mainWindow.Open(new EmptyWindowState());
            
            mainWindow.Username.OnTextChange("MyUsername");
            mainWindow.PlayButton.Click();

            windowManagerMock.AssertLastOpenedWindow<GamesManagementWindow, GamesManagementWindowState>(
                s =>
                {
                    AssertExt.Equal(s.Username.Value, "MyUsername");
                }
            );
        }

    }
}