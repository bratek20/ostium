using B20.Events.Api;
using B20.Ext;
using B20.Frontend.Element;
using B20.Frontend.Elements;
using B20.Frontend.Traits;
using GameModule.Api;


namespace Ostium.Logic
{
    public partial class RowVM: ElementVm<Row>
    {
        public OptionalElementVm<CreatureCardVm, CreatureCard> Card { get; }
        
        protected override void OnUpdate()
        {
            Card.Update(Model.GetCard());
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
        
        public bool HasCard => Model.GetCard().IsPresent();

        public bool ContainsCard(CreatureCardVm card)
        {
            return HasCard && Card.Element.Id.Equals(card.Id);
        }
    }
}