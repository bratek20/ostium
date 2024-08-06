using B20.Events.Api;
using B20.Frontend.Elements;
using GameSetup.Api;

namespace Ostium.Logic
{
    public class GameVM: ElementVM<Game>
    {
        public TableVM Table { get; private set; }
        public HandVM Hand { get; private set; }
        
        public GameVM(EventPublisher eventPublisher)
        {
            Table = new TableVM();
            Hand = new HandVM(eventPublisher);
        }
        
        protected override void OnUpdate()
        {
            //Table.Update(Model.GetTable());
            Hand.Update(Model.GetHand());
        }
    }
}