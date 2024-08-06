using B20.Events.Api;
using B20.Frontend.Elements;
using GameComponents.Api;
using GameSetup.Api;

namespace Ostium.Logic
{
    public class HandVM: ElementVM<Hand>
    {
        public CreatureCardVM Card1 { get; private set; }
        public CreatureCardVM Card2 { get; private set; }
        
        public HandVM(EventPublisher eventPublisher)
        {
            Card1 = new CreatureCardVM(eventPublisher);
            Card2 = new CreatureCardVM(eventPublisher);
        }

        protected override void OnUpdate()
        {
            Card1.Update(Model.GetCards()[0]);
            Card2.Update(Model.GetCards()[1]);
        }
    }
}