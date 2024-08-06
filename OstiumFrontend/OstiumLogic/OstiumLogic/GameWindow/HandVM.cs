using B20.Events.Api;
using B20.Frontend.Elements;
using GameComponents.Api;
using GameSetup.Api;

namespace Ostium.Logic
{
    public class HandVM: ElementVM<Hand>
    {
        CreatureCardVM card1;
        CreatureCardVM card2;
        
        public HandVM(EventPublisher eventPublisher)
        {
            card1 = new CreatureCardVM(eventPublisher);
            card2 = new CreatureCardVM(eventPublisher);
        }

        protected override void OnUpdate()
        {
            card1.Update(Model.GetCards()[0]);
            card2.Update(Model.GetCards()[1]);
        }
    }
}