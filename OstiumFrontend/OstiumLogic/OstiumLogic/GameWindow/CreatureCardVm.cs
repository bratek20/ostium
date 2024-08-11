using B20.Events.Api;
using B20.Frontend.Element;
using B20.Frontend.Elements;
using B20.Frontend.Traits;
using GameComponents.Api;

namespace Ostium.Logic
{
    public class CreatureCardVm: ElementVm<CreatureCard>
    {
        public LabelVm Name { get; } = new LabelVm();
        public VisibleVm Selected { get; } = new VisibleVm();

        public CreatureCardId Id => Model.GetId();
        
        public CreatureCardVm(EventPublisher publisher)
        {
            AddTrait(new Clickable(publisher));
            AddTrait(new Draggable(publisher));
            SetSelected(false);
        }
        
        public void SetSelected(bool selected)
        {
            Selected.Update(selected);
        }
        
        protected override void OnUpdate()
        {
            Name.Update(Model.GetId().Value);
        }
    }
}