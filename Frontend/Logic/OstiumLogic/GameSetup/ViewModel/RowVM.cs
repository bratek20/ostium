using B20.Events.Api;
using B20.Ext;
using B20.Frontend.Element;
using B20.Frontend.Elements;
using B20.Frontend.Traits;
using GameComponents.Api;
using GameSetup.Api;

namespace Ostium.Logic
{
    public class RowVM: ElementVm<Optional<CreatureCard>>
    {
        public RowType Type { get; }
        public OptionalElementVm<CreatureCardVm, CreatureCard> Card { get; }
        
        public bool HasCard => Model.IsPresent();

        public RowVM(RowType type, EventPublisher publisher)
        {
            AddTrait(new WithRect());

            Type = type;
            Card = new OptionalElementVm<CreatureCardVm, CreatureCard>(new CreatureCardVm(publisher));
        }
        
        public bool ContainsCard(CreatureCardVm card)
        {
            return HasCard && Card.Element.Id.Equals(card.Id);
        }
        
        protected override void OnUpdate()
        {
            Card.Update(Model);
        }
    }
}