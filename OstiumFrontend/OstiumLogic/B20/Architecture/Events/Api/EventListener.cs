using System;

namespace B20.Events.Api
{
    public interface EventListener
    {
        
    }
    
    public interface EventListener<TEvent> : EventListener where TEvent : Event
    {
        void HandleEvent(TEvent e);
    }
}