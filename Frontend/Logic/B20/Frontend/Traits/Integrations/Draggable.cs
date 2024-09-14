using B20.Events.Api;
using B20.Frontend.Element;
using B20.Frontend.Elements;
using B20.Frontend.Postion;

namespace B20.Frontend.Traits
{
    public class ElementDragStartedEvent : Event
    {
        public ElementVm Element { get; }
        public Position2d Position { get; }
        
        public ElementDragStartedEvent(ElementVm element, Position2d position)
        {
            Element = element;
            Position = position;
        }
    }

    public class ElementDragEndedEvent : Event
    {
        public ElementVm Element { get; }
        public Position2d Position { get; }
        
        public ElementDragEndedEvent(ElementVm element, Position2d position)
        {
            Element = element;
            Position = position;
        }
    }

    public class Draggable : Trait
    {
        private EventPublisher publisher;
        private WithPosition2d Position => Owner.GetTrait<WithPosition2d>();
        private Position2d startPosition;
        
        public Draggable(EventPublisher publisher)
        {
            this.publisher = publisher;
        }

        public void StartDrag(Position2d p)
        {
            startPosition = p;
            Position.Update(p);
            
            publisher.Publish(new ElementDragStartedEvent(Owner, p));
        }
        
        public void OnDrag(Position2d p)
        {
            Position.Update(p);
        }

        public void EndDrag(Position2d p)
        {
            Position.Update(startPosition);
            
            publisher.Publish(new ElementDragEndedEvent(Owner, p));
        }
    }
}
