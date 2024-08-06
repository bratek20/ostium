using System.Collections.Generic;
using B20.Events.Api;

namespace B20.Events.Impl
{
    public class EventPublisherLogic : EventPublisher
    {
        private readonly List<EventListener> listeners;

        public EventPublisherLogic(List<EventListener> listeners)
        {
            this.listeners = listeners;
        }

        public void Publish<TEvent>(TEvent e) where TEvent : Event
        {
            foreach (var listener in listeners)
            {
                if (listener is EventListener<TEvent> typedListener)
                {
                    typedListener.HandleEvent(e);
                }
            }
        }
    }
}