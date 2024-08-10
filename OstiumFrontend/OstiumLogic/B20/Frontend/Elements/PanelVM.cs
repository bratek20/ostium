using B20.Events.Api;

namespace B20.Frontend.Element
{
    public class PanelClickedEvent: Event
    {
        public PanelVM Panel {get; private set;}
        
        public PanelClickedEvent(PanelVM panel)
        {
            Panel = panel;
        }
    }

    public interface PanelVM: ElementVM
    {
        bool Clickable { get; }
        
        void Click();
    }

    public class PanelVM<T>: ElementVM<T>, PanelVM where T: class
    {
        public bool Clickable { get; protected set; } = false;
        
        private EventPublisher publisher;
        
        public PanelVM(EventPublisher publisher)
        {
            this.publisher = publisher;
        }
        
        public void Click()
        { 
            publisher.Publish(new PanelClickedEvent(this));
        }
    }
}