using System;
using System.Collections.Generic;
using B20.Frontend.Postion;
using B20.Frontend.Traits;
using B20.Frontend.Windows.Api;
using B20.Logic.Utils;
using B20.Tests.Frontend.Traits.Fixtures;
using GameComponents;
using GameModule.Api;
using GameModule.ViewModel;
using Main.ViewModel;
using Xunit;

namespace Ostium.Logic.Tests
{
    public class OstiumLogicTest
    {
        private Scenarios scenarios = new Scenarios();
        
        [Fact]
        public void ShouldStartOnMainScreenAndSwitchToGameScreen()
        {
            var c = scenarios.Setup();

            //should not throw
            c.WindowManager.Get<MainWindow>();
            c.WindowManager.Get<GameWindow>();
            
            c.Logic.Start();

            Assert.IsType<MainWindow>(c.WindowManager.GetCurrent());
            
            //Clicking play button
            c.WindowManager.Get<MainWindow>().PlayButton.Click();

            Assert.IsType<GameWindow>(c.WindowManager.GetCurrent());
        }

        [Fact]
        public void ShouldPlayCards()
        {
            var c = scenarios.InGameWindow(i =>
            {
                i.AttackRowRect = new Rect(new Position2d(10, 10), new Position2d(1, 1));
                i.DefenseRowRect = new Rect(new Position2d(20, 20), new Position2d(1, 1));
                i.Game = g =>
                {
                    g.Hand = h =>
                    {
                        h.Cards = ListUtils.Of<Action<Builders.CreatureCardDef>>
                        (
                            card => { card.Id = "Mouse1"; },
                            card => { card.Id = "Mouse2"; }
                        );
                    };
                };
            });
            
            //Playing first card on attack row
            var card1Name = c.FirstCardInHand.Name.Model;
            Assert.Equal("Mouse1", card1Name);
            
            TraitHelpers.StartDrag(c.FirstCardInHand, new Position2d(0, 0));
            AssertSelectedCard(c, "Mouse1");
            TraitHelpers.EndDrag(c.FirstCardInHand, new Position2d(10, 10));
            AssertNoCardSelected(c);
            
            c.GameApiMock.AssertPlayCardLastCall("Mouse1", RowType.ATTACK);
            
            //Playing second card on defense row
            TraitHelpers.StartDrag(c.SecondCardInHand, new Position2d(0, 0));
            TraitHelpers.EndDrag(c.SecondCardInHand, new Position2d(20, 20));
            
            c.GameApiMock.AssertPlayCardLastCall("Mouse2", RowType.DEFENSE);
        }
        
        [Fact]
        public void ShouldMovePlayedCardBetweenRowsByDragging()
        {
            var c = scenarios.InGameWindow(i =>
            {
                i.AttackRowRect = new Rect(new Position2d(10, 10), new Position2d(1, 1));
                i.DefenseRowRect = new Rect(new Position2d(20, 20), new Position2d(1, 1));
                i.Game = g =>
                {
                    g.Table = t => 
                    {
                        t.DefenseRow = r => { r.Id = "Mouse1"; };
                    };
                };
            });
           
            AssertCardInRow(c.DefenseRow, "Mouse1");
            
            TraitHelpers.StartDrag(c.CardInDefenseRow, new Position2d(20, 20));
            AssertSelectedCard(c, "Mouse1");
            TraitHelpers.EndDrag(c.CardInDefenseRow, new Position2d(10, 10));
            AssertNoCardSelected(c);
            
            c.GameApiMock.AssertMoveCardLastCall("Mouse1", RowType.DEFENSE, RowType.ATTACK);
        }

        [Fact]
        public void ShouldSwapPlayedCards()
        {
            var c = scenarios.InGameWindow(i =>
            {
                i.AttackRowRect = new Rect(new Position2d(10, 10), new Position2d(1, 1));
                i.DefenseRowRect = new Rect(new Position2d(20, 20), new Position2d(1, 1));
                i.Game = g =>
                {
                    g.Table = t => 
                    {
                        t.AttackRow = r => { r.Id = "Mouse1"; };
                        t.DefenseRow = r => { r.Id = "Mouse2"; };
                    };
                };
            });
            AssertCardInRow(c.AttackRow, "Mouse1");
            AssertCardInRow(c.DefenseRow, "Mouse2");
            
            TraitHelpers.StartDrag(c.CardInAttackRow, new Position2d(10, 10));
            TraitHelpers.EndDrag(c.CardInAttackRow, new Position2d(20, 20));
            c.GameApiMock.AssertMoveCardLastCall("Mouse1", RowType.ATTACK, RowType.DEFENSE);
        }
        
        [Fact]
        public void EndingDragInTheSameRowDoesNothing()
        {
            var c = scenarios.InGameWindow(i =>
            {
                i.AttackRowRect = new Rect(new Position2d(10, 10), new Position2d(1, 1));
                i.DefenseRowRect = new Rect(new Position2d(20, 20), new Position2d(1, 1));
                i.Game = g =>
                {
                    g.Table = t => 
                    {
                        t.AttackRow = r => { r.Id = "Mouse1"; };
                    };
                };
            });
            AssertCardInRow(c.AttackRow, "Mouse1");
            
            TraitHelpers.StartDrag(c.CardInAttackRow, new Position2d(10, 10));
            TraitHelpers.EndDrag(c.CardInAttackRow, new Position2d(10, 10));

            c.GameApiMock.AssertNoCalls();
        }
        
        [Fact]
        public void InitialCardsInHandAreNotSelected()
        {
            var c = scenarios.InGameWindow();
            
            c.CardsInHand.ForEach(card =>
            {
                Assert.False(card.Selected.Model);
            });
        }
        
        void AssertCardInRow(RowVm row, string cardName)
        {
            Assert.True(row.HasCard);
            Asserts.AssertCreatureCardId(row.Model.GetCard().Get().GetId(), cardName);
        }
        
        void AssertRowEmpty(RowVm row)
        {
            Assert.False(row.HasCard);
        }
        
        void AssertSelectedCard(Scenarios.InGameWindowContext c, string cardName)
        {
            Assert.True(c.SelectedCard.IsPresent());
            Assert.True(c.SelectedCard.Get().Selected.Model);
            Asserts.AssertCreatureCardId(c.SelectedCard.Get().Id, cardName);
        }
        
        void AssertNoCardSelected(Scenarios.InGameWindowContext c)
        {
            Assert.False(c.SelectedCard.IsPresent());
        }
    }
}