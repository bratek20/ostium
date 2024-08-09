using B20.Events.Api;
using B20.Frontend.Elements;
using GameComponents.Api;
using GameSetup.Api;

namespace Ostium.Logic
{
    public class GameVM: ElementVM<Game>, EventListener<PanelClickedEvent>
    {
        public TableVM Table { get; }
        public HandVM Hand { get; }

        private GameSetupApi gameSetupApi;
        public GameVM(GameSetupApi gameSetupApi, EventPublisher eventPublisher)
        {
            this.gameSetupApi = gameSetupApi;
            eventPublisher.AddListener(this);
            
            Table = new TableVM(eventPublisher);
            Hand = new HandVM(eventPublisher);
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

        private CreatureCardId clickedCardId;

        private bool TryMove(RowType clickedRow, RowVM otherRow)
        {
            if (otherRow.HasCard && clickedCardId.Equals(otherRow.Card.Model.GetId()))
            {
                Update(gameSetupApi.MoveCard(clickedCardId, otherRow.Type, clickedRow));
                return true;
            }
            return false;
        }
        
        public void HandleEvent(PanelClickedEvent e)
        {
            if (e.Panel is CreatureCardVM card)
            {
                clickedCardId = card.Model.GetId();
            }
            if (e.Panel is RowVM row)
            {
                var rowType = row.Type;
                var otherRow = rowType == RowType.ATTACK ? Table.DefenseRow : Table.AttackRow;
                if (!TryMove(rowType, otherRow))
                {
                    Update(gameSetupApi.PlayCard(clickedCardId, rowType));
                }
            }
        }
    }
}