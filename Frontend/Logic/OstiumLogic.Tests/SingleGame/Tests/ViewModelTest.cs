using System;
using B20.Frontend.Postion;
using B20.Frontend.UiElements.Utils;
using B20.Tests.Frontend.Traits.Fixtures;
using Ostium.Logic.Tests;
using SingleGame.Api;
using SingleGame.ViewModel;
using Xunit;

namespace SingleGame.Tests
{
    public class ViewModelTest
    {
        private Scenarios scenarios = new Scenarios();

        [Fact]
        public void ShouldLoadStateAndPlayCards()
        {
            var c = scenarios.InGameWindow(i =>
            {
                i.Game = g =>
                {
                    g.MyHand = h =>
                    {
                        h.Cards = ListUtils.Of<Action<SingleGameBuilders.CreatureCardDef>>
                        (
                            card => { card.Id = "Mouse1"; },
                            card => { card.Id = "Mouse2"; }
                        );
                    };
                };
            });
            
            c.GameApiMock.AssertGetStateLastCall(42, "testUser");
            
            //Playing first card on attack row
            var card1Name = c.FirstCardInHand.Id.Model;
            Assert.Equal("Mouse1", card1Name);
            
            TraitsHelpers.StartDrag(c.FirstCardInHand);
            AssertSelectedCard(c, "Mouse1");
            TraitsHelpers.EndDragIn(c.FirstCardInHand, c.AttackRow);
            AssertNoCardSelected(c);
            
            c.GameApiMock.AssertPlayCardLastCall(
                42,
                "testUser",
                "Mouse1", 
                RowType.ATTACK
                );
            
            //Playing second card on defense row
            TraitsHelpers.DragTo(c.SecondCardInHand, c.DefenseRow);
            
            c.GameApiMock.AssertPlayCardLastCall(
                42,
                "testUser",
                "Mouse2", 
                RowType.DEFENSE
            );
        }
        
        [Fact]
        public void ShouldGetStateEvery5Seconds()
        {
            var c = scenarios.InGameWindow();
            
            c.GameApiMock.AssertGetStateCallsNumber(1); // one right away
            
            c.TimerApi.Progress(4999);
            c.GameApiMock.AssertGetStateCallsNumber(1); // not yet
            
            c.TimerApi.Progress(1);
            c.GameApiMock.AssertGetStateCallsNumber(2); // now
        }
        
        [Fact]
        public void ShouldMovePlayedCardBetweenRowsByDragging()
        {
            var c = scenarios.InGameWindow(i =>
            {
                i.Game = g =>
                {
                    g.Table = t => 
                    {
                        t.MySide = s => 
                        {
                            s.DefenseRow = r => { r.Id = "Mouse1"; };
                        };
                    };
                };
            });
           
            AssertCardInRow(c.DefenseRow, "Mouse1");

            TraitsHelpers.StartDrag(c.CardInDefenseRow);
            AssertSelectedCard(c, "Mouse1");
            TraitsHelpers.EndDragIn(c.CardInDefenseRow, c.AttackRow);
            AssertNoCardSelected(c);
            
            c.GameApiMock.AssertMoveCardLastCall(
                42,
                "testUser",
                "Mouse1", 
                RowType.DEFENSE, 
                RowType.ATTACK
                );
        }

        [Fact]
        public void ShouldSwapPlayedCards()
        {
            var c = scenarios.InGameWindow(i =>
            {
                i.Game = g =>
                {
                    g.Table = t => 
                    {
                        t.MySide = s => 
                        {
                            s.AttackRow = r => { r.Id = "Mouse1"; };
                            s.DefenseRow = r => { r.Id = "Mouse2"; };
                        };
                    };
                };
            });
            AssertCardInRow(c.AttackRow, "Mouse1");
            AssertCardInRow(c.DefenseRow, "Mouse2");
            
            TraitsHelpers.StartDrag(c.CardInAttackRow);
            TraitsHelpers.EndDragIn(c.CardInAttackRow, c.DefenseRow);
            c.GameApiMock.AssertMoveCardLastCall(
                42,
                "testUser",
                "Mouse1", 
                RowType.ATTACK, 
                RowType.DEFENSE
            );
        }
        
        [Fact]
        public void EndingDragInTheSameRowDoesNothing()
        {
            var c = scenarios.InGameWindow(i =>
            {
                i.Game = g =>
                {
                    g.Table = t => 
                    {
                        t.MySide = s => 
                        {
                            s.AttackRow = r => { r.Id = "Mouse1"; };
                        };
                    };
                };
            });
            AssertCardInRow(c.AttackRow, "Mouse1");
            
            TraitsHelpers.StartDrag(c.CardInAttackRow);
            TraitsHelpers.EndDragAt(c.CardInAttackRow, c.CardInAttackRow);

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
            SingleGameAsserts.AssertCreatureCardId(row.Model.GetCard().Get().GetId(), cardName);
        }
        
        void AssertRowEmpty(RowVm row)
        {
            Assert.False(row.HasCard);
        }
        
        void AssertSelectedCard(Scenarios.InGameWindowContext c, string cardName)
        {
            Assert.True(c.SelectedCard.IsPresent());
            Assert.True(c.SelectedCard.Get().Selected.Model);
            SingleGameAsserts.AssertCreatureCardId(c.SelectedCard.Get().ModelId, cardName);
        }
        
        void AssertNoCardSelected(Scenarios.InGameWindowContext c)
        {
            Assert.False(c.SelectedCard.IsPresent());
        }
    }
}