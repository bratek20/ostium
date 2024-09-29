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
        private MainWindow mainWindow;
        private WindowManagerMock windowManagerMock;
        
        public MainViewModelTest()
        {
            var c = ContextsFactory.CreateBuilder()
                .WithModules(
                    new ViewModelTesting(),
                    
                    //tested module
                    new MainViewModel()
                ).Build();
            
            mainWindow = c.Get<MainWindow>();
            windowManagerMock = c.Get<WindowManagerMock>();

            mainWindow.Open(new EmptyWindowState());
        }
        
        [Fact]
        public void ShouldOpenManagementWindowPassingUsernameWhenPlayClicked()
        {
            mainWindow.Username.OnTextChange("MyUsername");
            mainWindow.PlayButton.Click();

            AssertGameManagementWindowOpened("MyUsername");
        }
        
        [Fact]
        public void ShouldUseDefaultUserIfNoTextChanged()
        {
            mainWindow.PlayButton.Click();

            AssertGameManagementWindowOpened("DefaultUser");
        }
        
        private void AssertGameManagementWindowOpened(string expectedUsername)
        {
            windowManagerMock.AssertLastOpenedWindow<GamesManagementWindow, GamesManagementWindowState>(
                s =>
                {
                    AssertExt.Equal(s.Username.Value, expectedUsername);
                }
            );
        }
    }
}