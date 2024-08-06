using B20.Events.Api;

namespace B20.Frontend.Elements.Api
{
    class PanelClickedEvent: Event
    {
        public Panel Panel {get; private set;}
        
        public PanelClickedEvent(Panel panel)
        {
            Panel = panel;
        }
    }
    
    public class Panel: ElementVM
    {
        public bool Clickable { get; protected set; } = false;
        
        private EventPublisher publisher;
        
        public Panel(EventPublisher publisher)
        {
            this.publisher = publisher;
        }
        
        public void Click()
        { 
            publisher.Publish(new PanelClickedEvent(this));
        }
    }
}