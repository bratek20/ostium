using B20.Events.Api;
using B20.Frontend.Elements.Api;
using GameComponents.Api;

namespace Ostium.Logic
{
    public class CreatureCardVM: PanelVM<CreatureCard>
    {
        public CreatureCardVM(EventPublisher publisher): base(publisher)
        {
            Clickable = true;
        }
    }
}