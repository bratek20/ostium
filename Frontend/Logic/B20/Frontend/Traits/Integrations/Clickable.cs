using B20.Events.Api;
using B20.Frontend.UiElements;

namespace B20.Frontend.Traits
{
    public class UiElementClickedEvent: Event
    {
        public UiElement Element { get; }
        public UiElementClickedEvent(UiElement element)
        {
            Element = element;
        }
    }
    
    public class Clickable: Trait
    {
        private EventPublisher publisher;
        public Clickable(EventPublisher publisher)
        {
            this.publisher = publisher;
        }
        
        public void Click()
        {
            publisher.Publish(new UiElementClickedEvent(Owner));
        }
    }
}