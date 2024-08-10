using B20.Events.Api;
using B20.Frontend.Element;
using B20.Frontend.Postion;

namespace B20.Frontend.Traits
{
    public class ElementDragStartedEvent : Event
    {
        public ElementVM Element { get; }
        public Position2D Position { get; }
        
        public ElementDragStartedEvent(ElementVM element, Position2D position)
        {
            Element = element;
            Position = position;
        }
    }

    public class ElementDragEndedEvent : Event
    {
        public ElementVM Element { get; }
        public Position2D Position { get; }
        
        public ElementDragEndedEvent(ElementVM element, Position2D position)
        {
            Element = element;
            Position = position;
        }
    }

    public class Draggable : Trait
    {
        private EventPublisher publisher;
        public Draggable(EventPublisher publisher)
        {
            this.publisher = publisher;
        }

        public void StartDrag(Position2D p)
        {
            publisher.Publish(new ElementDragStartedEvent(Owner, p));
        }

        public void EndDrag(Position2D p)
        {
            publisher.Publish(new ElementDragEndedEvent(Owner, p));
        }
    }
}
