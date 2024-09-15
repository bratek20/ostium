using System.Collections.Generic;
using B20.Events.Api;
using B20.Ext;
using B20.Frontend.Postion;
using B20.Frontend.Traits;
using B20.Logic.Utils;
using GamesManagement.Api;
using SingleGame.Api;
using User.Api;

namespace SingleGame.ViewModel
{
    class GameElementDragStartedListener: EventListener<ElementDragStartedEvent>
    {
        private GameVm game;
        public GameElementDragStartedListener(GameVm game)
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
        private GameVm game;
        public GameElementDragEndedListener(GameVm game)
        {
            this.game = game;
        }
        
        public void HandleEvent(ElementDragEndedEvent e)
        {
            game.HandleElementDragEnded(e);
        }
    }

    public partial class GameVm
    {
        private SingleGameApi singleGameApi;
        public GameVm(SingleGameApi singleGameApi, EventPublisher eventPublisher)
        {
            this.singleGameApi = singleGameApi;
            eventPublisher.AddListener(new GameElementDragStartedListener(this));
            eventPublisher.AddListener(new GameElementDragEndedListener(this));
        }
        
        public void UpdateState()
        {
            Update(singleGameApi.GetState(new GameId(1), new Username("x")));
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
            return MyHand.Contains(card);
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
                    var game = singleGameApi.PlayCard(new GameId(1), new Username("x"), SelectedCard.Get().Id, row.Type);
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
                    var otherRow = row.Type == RowType.ATTACK ? Table.MySide.DefenseRow : Table.MySide.AttackRow;
                    var game = singleGameApi.MoveCard(new GameId(1), new Username("x"), SelectedCard.Get().Id, otherRow.Type, row.Type);
                    Update(game);
                });
            }
            SelectedCard = Optional<CreatureCardVm>.Empty();
        }

        private List<RowVm> AllRows()
        {
            return new List<RowVm> { Table.MySide.AttackRow, Table.MySide.DefenseRow };
        }
        
        private Optional<RowVm> FindRowWithPointInside(Position2d p)
        {
            return ListUtils.Find(AllRows(), row => row.GetTrait<WithRect>().Rect.IsInside(p));
        }
    }
}