using System;
using System.Collections.Generic;
using B20.Events.Api;
using Xunit;

namespace B20.Tests.Architecture.Events.Fixtures
{
    public class EventPublisherMock: EventPublisher
    {
        private List<Event> publishedEvents = new List<Event>();
        
        public void Publish<TEvent>(TEvent e) where TEvent : Event
        {
            publishedEvents.Add(e);
        }

        public void AssertOneEventPublished<T>(Func<T, bool> eventCheck) where T: Event
        {
            Assert.Equal(publishedEvents.Count, 1);
            Assert.Equal(true, publishedEvents[0] is T);
            Assert.Equal(true, eventCheck((T)publishedEvents[0]));
        }

        public void AddListener<TEvent>(EventListener<TEvent> listener) where TEvent : Event
        {
            throw new System.NotImplementedException();
        }
        
        public void Reset()
        {
            publishedEvents.Clear();
        }
    }
}