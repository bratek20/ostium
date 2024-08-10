using B20.Events.Api;
using B20.Frontend.Element;

namespace B20.Frontend.Traits
{
    public interface Position
    {
        
    }
    
    public class ElementDragStartedEvent : Event
    {
        public ElementVM Element { get; }
        public Position Position { get; }
        
        public ElementDragStartedEvent(ElementVM element, Position position)
        {
            Element = element;
            Position = position;
        }
    }

    public class ElementDragEndedEvent : Event
    {
        public ElementVM Element { get; }
        public Position Position { get; }
        
        public ElementDragEndedEvent(ElementVM element, Position position)
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

        public void StartDrag(Position p)
        {
            publisher.Publish(new ElementDragStartedEvent(Owner, p));
        }

        public void EndDrag(Position p)
        {
            publisher.Publish(new ElementDragEndedEvent(Owner, p));
        }
    }
}
