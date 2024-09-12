using B20.Events.Api;
using B20.Frontend.Elements;
using B20.Frontend.Traits;
using GameModule.Api;

namespace GameModule.ViewModel
{
    public partial class CreatureCardVm
    {
        public Position2dVm Position { get; } = new Position2dVm();
        
        public CreatureCardVm(EventPublisher publisher)
        {
            AddTrait(new Draggable(publisher, Position));
        }
        
        public CreatureCardId Id => Model.GetId();
        
        public void SetSelected(bool selected)
        {
            Selected.Update(selected);
        }
    }
    
    public partial class RowVm
    {
        public RowType Type => Model.GetType();
        public RowVm()
        {
            AddTrait(new WithRect());
        }
        
        public bool HasCard => Model.GetCard().IsPresent();

        public bool ContainsCard(CreatureCardVm card)
        {
            return HasCard && Card.Element.Id.Equals(card.Id);
        }
    }
    
    public partial class HandVm
    {
        public bool Contains(CreatureCardVm card)
        {
            return Model.GetCards().Exists(c => c.GetId().Equals(card.Model.GetId()));
        }
    }
}