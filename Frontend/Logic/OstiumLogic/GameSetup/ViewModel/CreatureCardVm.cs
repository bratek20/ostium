using B20.Events.Api;
using B20.Frontend.Element;
using B20.Frontend.Elements;
using B20.Frontend.Traits;
using GameComponents.Api;

namespace Ostium.Logic
{
    public partial class CreatureCardVm: ElementVm<CreatureCard>
    {
        public LabelVm Name { get; } = new LabelVm();
        public VisibleVm Selected { get; } = new VisibleVm();
        
        protected override void OnUpdate()
        {
            Name.Update(Model.GetId().Value);
        }
    }

    public partial class CreatureCardVm
    {
        public Position2dVm Position { get; } = new Position2dVm();
        
        public CreatureCardVm(EventPublisher publisher)
        {
            AddTrait(new Draggable(publisher, Position));
        }
        
        public CreatureCardId Id => Model.GetId();
        
        public void SetSelected(bool selected)
        {
            Selected.Update(selected);
        }
    }
}