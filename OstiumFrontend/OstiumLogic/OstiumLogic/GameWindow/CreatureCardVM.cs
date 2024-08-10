using B20.Events.Api;
using B20.Frontend.Element;
using GameComponents.Api;

namespace Ostium.Logic
{
    public class CreatureCardVM: PanelVM<CreatureCard>
    {
        public LabelVM Name { get; } = new LabelVM();
        
        public CreatureCardVM(EventPublisher publisher): base(publisher)
        {
            Clickable = true;
        }
        
        protected override void OnUpdate()
        {
            Name.Update(Model.GetId().Value);
        }
    }
}