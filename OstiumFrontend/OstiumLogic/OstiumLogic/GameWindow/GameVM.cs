using B20.Events.Api;
using B20.Frontend.Elements;
using GameSetup.Api;

namespace Ostium.Logic
{
    public class GameVM: ElementVM<Game>
    {
        private TableVM table;
        private HandVM hand;
        
        public GameVM(EventPublisher eventPublisher)
        {
            table = new TableVM();
            hand = new HandVM(eventPublisher);
        }
    }
}