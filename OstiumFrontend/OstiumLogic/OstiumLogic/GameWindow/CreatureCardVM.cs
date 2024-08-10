using B20.Events.Api;
using B20.Frontend.Element;
using B20.Frontend.Traits;
using GameComponents.Api;

namespace Ostium.Logic
{
    public class CreatureCardVM: ElementVM<CreatureCard>
    {
        public LabelVM Name { get; } = new LabelVM();

        public CreatureCardVM(EventPublisher publisher)
        {
            AddTrait(new Clickable(publisher));
            AddTrait(new Draggable(publisher));
            AddTrait(new WithRect());
        }
        
        protected override void OnUpdate()
        {
            Name.Update(Model.GetId().Value);
        }
        
        public void Click()
        {
            GetTrait<Clickable>().Click();
        }
    }
}