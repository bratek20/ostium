using System.Collections.Generic;
using B20.Architecture.Exceptions;
using B20.Events.Api;
using B20.Ext;
using B20.Frontend.Element;
using B20.Frontend.Postion;
using B20.Frontend.Traits;
using B20.Logic.Utils;
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

        private Optional<CreatureCardVm> _selectedCard = Optional<CreatureCardVm>.Empty();

        public Optional<CreatureCardVm> SelectedCard
        {
            get => _selectedCard;
            private set
            {
                _selectedCard.Let(c => c.SetSelected(false));
                _selectedCard = value;
                _selectedCard.Let(c => c.SetSelected(true));
            }
        }
        
        private bool TryMove(RowType clickedRow, RowVM otherRow)
        {
            return SelectedCard.Map(card =>
            {
                var cardId = card.Model.GetId();
                if (otherRow.HasCard && cardId.Equals(otherRow.Card.Model.GetId()))
                {
                    Update(gameSetupApi.MoveCard(cardId, otherRow.Type, clickedRow));
                    SelectedCard = Optional<CreatureCardVm>.Empty();
                    return true;
                }
                return false;
            }).OrElse(false);
        }

        private bool IsInHand(CreatureCardVm card)
        {
            return Hand.Contains(card);
        }
        
        private bool IsOnTable(CreatureCardVm card)
        {
            return Table.AttackRow.Contains(card) || Table.DefenseRow.Contains(card);
        }
        
        private RowType GetRowType(CreatureCardVm card)
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
                var optPreviousClickedCard = SelectedCard;
                SelectedCard = Optional<CreatureCardVm>.Of(card);

                optPreviousClickedCard.Let(previousClickedCard =>
                {
                    if(IsOnTable(previousClickedCard) && IsOnTable(SelectedCard.Get()))
                    {
                        Update(gameSetupApi.MoveCard(SelectedCard.Get().Model.GetId(), GetRowType(SelectedCard.Get()), GetRowType(previousClickedCard)));
                        SelectedCard = Optional<CreatureCardVm>.Empty();
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
                        Update(gameSetupApi.PlayCard(c.Model.GetId(), rowType));    
                    });
                    SelectedCard = Optional<CreatureCardVm>.Empty();
                }
            }
        }

        public void HandleElementDragStarted(ElementDragStartedEvent elementDragStartedEvent)
        {
            SelectedCard = Optional<CreatureCardVm>.Of(elementDragStartedEvent.Element as CreatureCardVm);
        }

        public void HandleElementDragEnded(ElementDragEndedEvent ev)
        {
            if (IsInHand(SelectedCard.Get()))
            {
                FindRowWithPointInside(ev.Position).Let(row =>
                {
                    var game = gameSetupApi.PlayCard(SelectedCard.Get().Id, row.Type);
                    Update(game);
                });
            }
            else // is in table
            {
                FindRowWithPointInside(ev.Position).Let(row =>
                {
                    var otherRow = row.Type == RowType.ATTACK ? Table.DefenseRow : Table.AttackRow;
                    var game = gameSetupApi.MoveCard(SelectedCard.Get().Id, otherRow.Type, row.Type);
                    Update(game);
                });
            }
            SelectedCard = Optional<CreatureCardVm>.Empty();
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