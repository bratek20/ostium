using B20.Events.Api;
using B20.Frontend.Elements;
using GameComponents.Api;

namespace Ostium.Logic
{
    public class RowVM: PanelVM<CreatureCard>
    {
        public CreatureCardVM Card { get; private set; }
        
        public RowVM(EventPublisher publisher): base(publisher)
        {
            Clickable = true;
            Card = new CreatureCardVM(publisher);
        }
        
        protected override void OnUpdate()
        {
            if (Model != null)
            {
                Card.Update(Model);    
            }
        }
    }
}