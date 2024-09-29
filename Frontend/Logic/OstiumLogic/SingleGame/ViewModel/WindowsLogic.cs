using System.Collections.Generic;
using B20.Events.Api;
using B20.Ext;
using B20.Frontend.Postion;
using B20.Frontend.Timer.Api;
using B20.Frontend.Traits;
using B20.Frontend.UiElements.Utils;
using GamesManagement.Api;
using SingleGame.Api;
using User.Api;

namespace SingleGame.ViewModel
{
    class GameElementDragStartedListener: EventListener<ElementDragStartedEvent>
    {
        private GameWindow window;
        public GameElementDragStartedListener(GameWindow window)
        {
            this.window = window;
        }
        
        public void HandleEvent(ElementDragStartedEvent e)
        {
            window.HandleElementDragStarted(e);
        }
    }
    
    class GameElementDragEndedListener: EventListener<ElementDragEndedEvent>
    {
        private GameWindow window;
        public GameElementDragEndedListener(GameWindow window)
        {
            this.window = window;
        }
        
        public void HandleEvent(ElementDragEndedEvent e)
        {
            window.HandleElementDragEnded(e);
        }
    }
    
    public partial class GameWindow
    {
        private SingleGameApi singleGameApi;
        private TimerApi timerApi;
        
        public GameWindow(SingleGameApi singleGameApi, EventPublisher eventPublisher, TimerApi timerApi)
        {
            this.singleGameApi = singleGameApi;
            this.timerApi = timerApi;
            
            eventPublisher.AddListener(new GameElementDragStartedListener(this));
            eventPublisher.AddListener(new GameElementDragEndedListener(this));
        }
        
        protected override void OnOpen()
        {
            Update();
            timerApi.Schedule(Update, 5);
        }
        
        private void Update()
        {
            GameState.Update(singleGameApi.GetState(State.GameId, State.User));
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
            return GameState.MyHand.Contains(card);
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
                    var game = singleGameApi.PlayCard(State.GameId, State.User, SelectedCard.Get().ModelId, row.Type);
                    GameState.Update(game);
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
                    var otherRow = row.Type == RowType.ATTACK ? GameState.Table.MySide.DefenseRow : GameState.Table.MySide.AttackRow;
                    var game = singleGameApi.MoveCard(State.GameId, State.User, SelectedCard.Get().ModelId, otherRow.Type, row.Type);
                    GameState.Update(game);
                });
            }
            SelectedCard = Optional<CreatureCardVm>.Empty();
        }

        private List<RowVm> AllRows()
        {
            return new List<RowVm> { GameState.Table.MySide.AttackRow, GameState.Table.MySide.DefenseRow };
        }
        
        private Optional<RowVm> FindRowWithPointInside(Position2d p)
        {
            return ListUtils.Find(AllRows(), row => row.GetTrait<WithRect>().Rect.IsInside(p));
        }
    }
}