using B20.Events.Api;
using B20.Ext;
using B20.Frontend.Element;
using B20.Frontend.Elements;
using B20.Frontend.Traits;
using GameComponents.Api;
using GameSetup.Api;

namespace Ostium.Logic
{
    public partial class RowVM: ElementVm<Optional<CreatureCard>>
    {
        public OptionalElementVm<CreatureCardVm, CreatureCard> Card { get; }
        
        protected override void OnUpdate()
        {
            Card.Update(Model);
        }
    }

    public partial class RowVM
    {
        public RowType Type { get; }
        public RowVM(RowType type, EventPublisher publisher)
        {
            AddTrait(new WithRect());

            Type = type;
            Card = new OptionalElementVm<CreatureCardVm, CreatureCard>(new CreatureCardVm(publisher));
        }
        
        public bool HasCard => Model.IsPresent();

        public bool ContainsCard(CreatureCardVm card)
        {
            return HasCard && Card.Element.Id.Equals(card.Id);
        }
    }
}