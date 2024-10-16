using B20.Architecture.Contexts.Context;
using B20.Tests.ExtraAsserts;
using B20.Tests.Frontend.TestHelpers;
using B20.Tests.Frontend.Traits.Fixtures;
using B20.Tests.Frontend.Windows.Context;
using B20.Tests.Frontend.Windows.Fixtures;
using GamesManagement.Context;
using GamesManagement.ViewModel;
using KaijuGame.ViewModel;
using Ostium.Logic.Tests.GamesManagement.Context;
using Ostium.Logic.Tests.GamesManagement.Fixtures;
using SingleGame.ViewModel;
using User.Api;
using Xunit;

namespace GamesManagement.Tests
{
    public class GamesManagementViewModelTest
    {
        private GamesManagementWindow window;
        private GamesManagementApiMock apiMock;
        private WindowManagerMock windowManagerMock;
        
        public GamesManagementViewModelTest()
        {
            var c = ContextsFactory.CreateBuilder()
                .WithModules(
                    new ViewModelTesting(),

                    new GameManagementMocks(),
                    
                    new GamesManagementViewModel()
                ).Build();
            
            window = c.Get<GamesManagementWindow>();
            apiMock = c.Get<GamesManagementApiMock>();
            windowManagerMock = c.Get<WindowManagerMock>();
            
            window.Open(new GamesManagementWindowState(new Username("testUser")));
        }

        [Fact]
        public void ShouldLoadCreatedGamesAndJoinGameOnClick()
        {
            AssertExt.ListCount(window.CreatedGames.Elements, 1);
            
            TraitsHelpers.Click(window.CreatedGames.Elements[0]);
            
            apiMock.AssertJoinCalled("testUser", 69);
            
            windowManagerMock.AssertLastOpenedWindow<GameWindow, GameWindowState>(
                state =>
                {
                    AssertExt.Equal(state.Token.GetGameId().Value, 69);
                    AssertExt.Equal(state.Token.GetUsername().Value, "testUser");
                });
        }
        
        [Fact]
        public void ShouldDeleteGameAndRefreshWhenDeleteButtonClicked()
        {
            apiMock.AssertGetAllCreatedCalled(1);

            window.CreatedGames.Elements[0].Delete.Click();
            
            apiMock.AssertDeleteCalled(69);
            apiMock.AssertGetAllCreatedCalled(2);
        }
        
        [Fact]
        public void ShouldCreateGameWhenButtonClickedAndGoToTheGame()
        {
            window.CreateGame.Click();
            
            apiMock.AssertCreateCalled(new Username("testUser"));
            
            windowManagerMock.AssertLastOpenedWindow<GameWindow, GameWindowState>(
                state =>
                {
                    AssertExt.Equal(state.Token.GetGameId().Value, 666);
                    AssertExt.Equal(state.Token.GetUsername().Value, "testUser");
                });
        }

    }
}