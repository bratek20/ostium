using System.Collections.Generic;
using B20.Events.Api;
using B20.Ext;
using B20.Frontend.Element;
using B20.Frontend.Postion;
using B20.Frontend.Traits;
using B20.Logic.Utils;
using GameModule.Api;

namespace Ostium.Logic
{
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
    
    public partial class GameVM: ElementVm<GameModule.Api.Game>
    {
        public TableVM Table { get; }
        public HandVm Hand { get; }

        protected override void OnUpdate()
        {
            Table.Update(Model.GetTable());
            Hand.Update(Model.GetHand());
        }
    }
    
    public partial class GameVM
    {
        private GameApi gameSetupApi;
        public GameVM(GameApi gameSetupApi, EventPublisher eventPublisher)
        {
            this.gameSetupApi = gameSetupApi;
            eventPublisher.AddListener(new GameElementDragStartedListener(this));
            eventPublisher.AddListener(new GameElementDragEndedListener(this));
            
            Table = new TableVM(eventPublisher);
            Hand = new HandVm(eventPublisher);
        }
        
        public void StartGame()
        {
            Update(gameSetupApi.StartGame());
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

        private bool IsInHand(CreatureCardVm card)
        {
            return Hand.Contains(card);
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
                    if (row.ContainsCard(SelectedCard.Get()))
                    {
                        return;
                    }
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