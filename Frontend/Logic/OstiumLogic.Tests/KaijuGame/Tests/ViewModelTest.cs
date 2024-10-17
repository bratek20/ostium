using System;
using System.Linq;
using B20.Architecture.Contexts.Api;
using B20.Architecture.Contexts.Context;
using B20.Tests.ExtraAsserts;
using B20.Tests.Frontend.TestHelpers;
using B20.Tests.Frontend.Traits.Fixtures;
using GamesManagement.Api;
using KaijuGame.Api;
using KaijuGame.Context;
using KaijuGame.ViewModel;
using Ostium.Logic.Tests.KaijuGame.Fixtures;
using Xunit;



namespace KaijuGame.Tests
{
    public class KaijuGameViewModelTest
    {
        private GameWindow window;
        private GameApiMock apiMock;

        public KaijuGameViewModelTest()
        {
            var c = ContextsFactory.CreateBuilder()
                .WithModules(
                    new ViewModelTesting(),

                    new KaijuGameMocks(),

                    new KaijuGameViewModel()
                ).Build();

            window = c.Get<GameWindow>();
            apiMock = c.Get<GameApiMock>();

            TraitsHelpers.PlaceElements(window);
            
            window.Open(new GameWindowState(new GameToken(666, "testUser")));
        }

        [Fact]
        public void ShouldLoadGameState()
        {
            AssertExt.Equal(window.GameState.Model.GetTurn(), 1);
        }
        
        [Fact]
        public void ShouldEndPhaseOnButtonClick()
        {
            window.EndPhaseButton.Click();
            
            apiMock.AssertEndPhaseCalled();
        }

        private CardVm FirstCardInHand => window.GameState.Hand.Cards.Elements.First();
        private TableVm Table => window.GameState.Table;
        
        [Fact]
        public void ShouldPlayCardIfDraggedToMySide()
        {
            
            
            TraitsHelpers.DragTo(
                FirstCardInHand,
                Table.MySide
                );
            
            apiMock.AssertPlayCardLastCall(0);
        }
        
        [Fact]
        public void ShouldNotPlayCardIfDraggedToOpponentSide()
        {
            TraitsHelpers.DragTo(
                FirstCardInHand, 
                Table.OpponentSide
            );
            
            apiMock.AssertPlayCardNotCalled();
        }
    }
}