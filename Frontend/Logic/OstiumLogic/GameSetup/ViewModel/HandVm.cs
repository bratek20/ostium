using B20.Events.Api;
using B20.Frontend.Element;
using B20.Frontend.Elements;
using GameComponents.Api;
using GameSetup.Api;

namespace Ostium.Logic
{
    public partial class HandVm: ElementVm<Hand>
    {
        public ElementListVm<CreatureCardVm, CreatureCard> Cards { get; }

        protected override void OnUpdate()
        {
            Cards.Update(Model.GetCards());
        }
    }

    public partial class HandVm
    {
        public HandVm(EventPublisher eventPublisher)
        {
            Cards = new ElementListVm<CreatureCardVm, CreatureCard>(() => new CreatureCardVm(eventPublisher));
        }
        
        public bool Contains(CreatureCardVm card)
        {
            return Model.GetCards().Exists(c => c.GetId().Equals(card.Model.GetId()));
        }
    }
}