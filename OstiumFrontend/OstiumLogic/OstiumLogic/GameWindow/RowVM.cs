using B20.Events.Api;
using B20.Frontend.Elements;
using GameComponents.Api;
using GameSetup.Api;

namespace Ostium.Logic
{
    public class RowVM: PanelVM<CreatureCard>
    {
        public RowType Type { get; private set; }
        public CreatureCardVM Card { get; private set; }
        
        public RowVM(RowType type, EventPublisher publisher): base(publisher)
        {
            Clickable = true;
            
            Type = type;
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