using B20.Events.Api;
using B20.Frontend.Element;
using B20.Frontend.Elements;
using GameComponents.Api;
using GameSetup.Api;

namespace Ostium.Logic
{
    public class HandVm: ElementVm<Hand>
    {
        public ElementListVm<CreatureCardVm, CreatureCard> Cards { get; }
        
        public HandVm(EventPublisher eventPublisher)
        {
            Cards = new ElementListVm<CreatureCardVm, CreatureCard>(() => new CreatureCardVm(eventPublisher));
        }
        
        public bool Contains(CreatureCardVm card)
        {
            return Model.GetCards().Exists(c => c.GetId().Equals(card.Model.GetId()));
        }

        protected override void OnUpdate()
        {
            Cards.Update(Model.GetCards());
        }
    }
}