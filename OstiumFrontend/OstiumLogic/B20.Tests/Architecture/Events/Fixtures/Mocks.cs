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

        public void AssertOneEventPublished(Event expected)
        {
            Assert.Equal(publishedEvents.Count, 1);
            Assert.Equal(publishedEvents[0], expected);
        }

        public void AddListener<TEvent>(EventListener<TEvent> listener) where TEvent : Event
        {
            throw new System.NotImplementedException();
        }
    }
}