using SingleGame.Api;

namespace SingleGame.ViewModel
{
    public partial class CreatureCardVm
    {
        public CreatureCardId Id => Model.GetId();
        
        public void SetSelected(bool selected)
        {
            Selected.Update(selected);
        }
    }
    
    public partial class RowVm
    {
        public RowType Type => Model.GetType();

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