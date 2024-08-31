using System.Collections.Generic;
using B20.Events.Api;

namespace B20.Events.Impl
{
    public class EventPublisherLogic : EventPublisher
    {
        private readonly List<EventListener> listeners = new List<EventListener>();

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

        public void AddListener<TEvent>(EventListener<TEvent> listener) where TEvent : Event
        {
            listeners.Add(listener);
        }
    }
}