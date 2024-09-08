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
        public OptionalCreatureCardVm Card { get; set; }
        
        protected override void OnUpdate()
        {
            Card.Update(Model.GetCard());
        }
    }

    public partial class RowVM
    {
        public RowType Type => RowType.ATTACK; //TODO-REF Model.GetType()
        public RowVM()
        {
            AddTrait(new WithRect());
        }
        
        public bool HasCard => Model.GetCard().IsPresent();

        public bool ContainsCard(CreatureCardVm card)
        {
            return HasCard && Card.Element.Id.Equals(card.Id);
        }
    }
}