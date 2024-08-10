using B20.Events.Api;
using B20.Frontend.Element;

namespace B20.Frontend.Traits
{
    public class ElementClickedEvent: Event
    {
        public ElementVM Element { get; }
        public ElementClickedEvent(ElementVM element)
        {
            Element = element;
        }
        
        protected bool Equals(ElementClickedEvent other)
        {
            return Equals(Element, other.Element);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ElementClickedEvent)obj);
        }

        public override int GetHashCode()
        {
            return (Element != null ? Element.GetHashCode() : 0);
        }
    }
    
    public class ClickableTrait: Trait
    {
        private EventPublisher publisher;
        public ClickableTrait(EventPublisher publisher)
        {
            this.publisher = publisher;
        }
        
        public void Click()
        {
            publisher.Publish(new ElementClickedEvent(Owner));
        }
    }
}