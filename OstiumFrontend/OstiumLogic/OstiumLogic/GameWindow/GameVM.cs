using System;
using System.Collections.Generic;
using B20.Architecture.Exceptions;
using B20.Events.Api;
using B20.Ext;
using B20.Frontend.Element;
using B20.Frontend.Postion;
using B20.Frontend.Traits;
using B20.Logic.Utils;
using GameComponents.Api;
using GameSetup.Api;

namespace Ostium.Logic
{
    class GameElementClickedListener: EventListener<ElementClickedEvent>
    {
        private GameVM game;
        public GameElementClickedListener(GameVM game)
        {
            this.game = game;
        }
        
        public void HandleEvent(ElementClickedEvent e)
        {
            game.HandleElementClicked(e);
        }
    }
    
    class GameElementDragStartedListener: EventListener<ElementDragStartedEvent>
    {
        private GameVM game;
        public GameElementDragStartedListener(GameVM game)
        {
            this.game = game;
        }
        
        public void HandleEvent(ElementDragStartedEvent e)
        {
            game.HandleElementDragStarted(e);
        }
    }
    
    class GameElementDragEndedListener: EventListener<ElementDragEndedEvent>
    {
        private GameVM game;
        public GameElementDragEndedListener(GameVM game)
        {
            this.game = game;
        }
        
        public void HandleEvent(ElementDragEndedEvent e)
        {
            game.HandleElementDragEnded(e);
        }
    }
    
    public class GameVM: ElementVm<Game>
    {
        public TableVM Table { get; }
        public HandVm Hand { get; }

        private GameSetupApi gameSetupApi;
        public GameVM(GameSetupApi gameSetupApi, EventPublisher eventPublisher)
        {
            this.gameSetupApi = gameSetupApi;
            eventPublisher.AddListener(new GameElementClickedListener(this));
            eventPublisher.AddListener(new GameElementDragStartedListener(this));
            eventPublisher.AddListener(new GameElementDragEndedListener(this));
            
            Table = new TableVM(eventPublisher);
            Hand = new HandVm(eventPublisher);
        }
        
        public void StartGame()
        {
            Update(gameSetupApi.StartGame());
        }
        
        protected override void OnUpdate()
        {
            Table.Update(Model.GetTable());
            Hand.Update(Model.GetHand());
        }

        public Optional<CreatureCardId> SelectedCard { get; private set; } = Optional<CreatureCardId>.Empty();
        private bool TryMove(RowType clickedRow, RowVM otherRow)
        {
            return SelectedCard.Map(card =>
            {
                if (otherRow.HasCard && card.Equals(otherRow.Card.Model.GetId()))
                {
                    Update(gameSetupApi.MoveCard(card, otherRow.Type, clickedRow));
                    SelectedCard = Optional<CreatureCardId>.Empty();
                    return true;
                }
                return false;
            }).OrElse(false);
        }

        private bool IsOnTable(CreatureCardId card)
        {
            return Table.AttackRow.Contains(card) || Table.DefenseRow.Contains(card);
        }
        
        private RowType GetRowType(CreatureCardId card)
        {
            if (Table.AttackRow.Contains(card))
            {
                return RowType.ATTACK;
            }
            if (Table.DefenseRow.Contains(card))
            {
                return RowType.DEFENSE;
            }
            throw new ApiException("card not found on table");
        }

        public void HandleElementClicked(ElementClickedEvent e)
        {
            if (e.Element is CreatureCardVm card)
            {
                var optPreviousClickedCardId = SelectedCard;
                SelectedCard = Optional<CreatureCardId>.Of(card.Model.GetId());

                optPreviousClickedCardId.Let(previousClickedCardId =>
                {
                    if(IsOnTable(previousClickedCardId) && IsOnTable(SelectedCard.Get()))
                    {
                        Update(gameSetupApi.MoveCard(SelectedCard.Get(), GetRowType(SelectedCard.Get()), GetRowType(previousClickedCardId)));
                        SelectedCard = Optional<CreatureCardId>.Empty();
                    }
                });
            }
            if (e.Element is RowVM row)
            {
                var rowType = row.Type;
                var otherRow = rowType == RowType.ATTACK ? Table.DefenseRow : Table.AttackRow;
                if (!TryMove(rowType, otherRow))
                {
                    SelectedCard.Let(c =>
                    {
                        Update(gameSetupApi.PlayCard(c, rowType));    
                    });
                    SelectedCard = Optional<CreatureCardId>.Empty();
                }
            }
        }

        public void HandleElementDragStarted(ElementDragStartedEvent elementDragStartedEvent)
        {
            SelectedCard = Optional<CreatureCardId>.Of((elementDragStartedEvent.Element as CreatureCardVm).Model.GetId());
        }

        public void HandleElementDragEnded(ElementDragEndedEvent ev)
        {
            FindRowWithPointInside(ev.Position).Let(row =>
            {
                var game = gameSetupApi.PlayCard(SelectedCard.Get(), row.Type);
                Update(game);
            });
            SelectedCard = Optional<CreatureCardId>.Empty();
        }

        private List<RowVM> AllRows()
        {
            return new List<RowVM> { Table.AttackRow, Table.DefenseRow };
        }
        
        private Optional<RowVM> FindRowWithPointInside(Position2d p)
        {
            return ListUtils.Find(AllRows(), row => row.GetTrait<WithRect>().Rect.IsInside(p));
        }
    }
}