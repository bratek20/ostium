using B20.Events.Api;
using B20.Frontend.Element;
using B20.Frontend.Traits;
using GameComponents.Api;

namespace Ostium.Logic
{
    public class CreatureCardVM: PanelVM<CreatureCard>
    {
        public LabelVM Name { get; } = new LabelVM();
        public ClickableTrait Clickable { get; }
        
        public CreatureCardVM(EventPublisher publisher)
        {
            Clickable = new ClickableTrait(publisher);
            Clickable.Init(this);
        }
        
        protected override void OnUpdate()
        {
            Name.Update(Model.GetId().Value);
        }
        
        public void Click()
        {
            Clickable.Click();
        }
    }
}