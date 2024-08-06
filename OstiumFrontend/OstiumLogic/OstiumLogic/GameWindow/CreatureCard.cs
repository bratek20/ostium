using B20.Events.Api;
using B20.Frontend.Elements.Api;

namespace Ostium.Logic
{
    public class CreatureCard: Panel
    {
        public CreatureCard(EventPublisher publisher): base(publisher)
        {
            Clickable = true;
        }
    }
}