using B20.Events.Api;
using B20.Frontend.Element;
using B20.Frontend.Elements;
using GameModule.Api;

namespace Ostium.Logic
{
    public partial class HandVm: ElementVm<Hand>
    {
        public CreateCardListVm Cards { get; set; }

        protected override void OnUpdate()
        {
            Cards.Update(Model.GetCards());
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