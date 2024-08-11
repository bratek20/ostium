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
        public Position2dVm Position { get; } = new Position2dVm();

        public CreatureCardId Id => Model.GetId();
        
        private bool _selected = false;
        
        public CreatureCardVm(EventPublisher publisher)
        {
            AddTrait(new Draggable(publisher, Position));
        }
        
        public void SetSelected(bool selected)
        {
            _selected = selected;
            Selected.Update(selected);
        }
        
        protected override void OnUpdate()
        {
            Name.Update(Model.GetId().Value);
            Selected.Update(_selected);
        }
    }
}