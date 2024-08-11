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
        private Position2dVm position;
        
        public Draggable(EventPublisher publisher, Position2dVm position)
        {
            this.publisher = publisher;
            this.position = position;
        }

        public void StartDrag(Position2d p)
        {
            publisher.Publish(new ElementDragStartedEvent(Owner, p));
        }
        
        public void OnDrag(Position2d p)
        {
            position.Update(p);
        }

        public void EndDrag(Position2d p)
        {
            publisher.Publish(new ElementDragEndedEvent(Owner, p));
        }
    }
}
