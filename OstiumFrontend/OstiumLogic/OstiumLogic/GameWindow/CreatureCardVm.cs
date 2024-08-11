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
        public Position2dVm Position { get; } = new Position2dVm();
        
        public CreatureCardVm(EventPublisher publisher)
        {
            AddTrait(new Draggable(publisher));
        }
        
        protected override void OnUpdate()
        {
            Name.Update(Model.GetId().Value);
        }
    }
}