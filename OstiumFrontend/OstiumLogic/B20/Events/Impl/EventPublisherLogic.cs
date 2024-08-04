using System.Collections.Generic;
using B20.Events.Api;

namespace B20.Logic.Events.Impl
{
    public class EventPublisherLogic : EventPublisher
    {
        private readonly List<EventListener> listeners;

        public EventPublisherLogic(List<EventListener> listeners)
        {
            this.listeners = listeners;
        }

        public void Publish(Event e)
        {
            foreach (var listener in listeners)
            {
                if (listener.GetEventType() == e.GetType())
                {
                    listener.HandleEvent(e);    
                }
            }
        }
    }
}