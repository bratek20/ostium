using System;
using System.Collections.Generic;
using B20.Events.Api;
using B20.Events.Impl;
using B20.Logic.Utils;
using Xunit;

namespace B20.Architecture.Events.Tests
{
    public class TestEvent : Event
    {
        public string Message { get; }

        public TestEvent(string message)
        {
            Message = message;
        }
    }

    public class OtherTestEvent : Event
    {
        public string Message { get; }

        public OtherTestEvent(string message)
        {
            Message = message;
        }
    }

    public class TestListener : EventListener<TestEvent>
    {
        public List<TestEvent> Events { get; }

        public TestListener()
        {
            Events = new List<TestEvent>();
        }

        public void HandleEvent(TestEvent e)
        {
            Events.Add(e);
        }
    }

    public class OtherTestListener : EventListener<OtherTestEvent>
    {
        public List<OtherTestEvent> Events { get; }

        public OtherTestListener()
        {
            Events = new List<OtherTestEvent>();
        }

        public void HandleEvent(OtherTestEvent e)
        {
            Events.Add(e);
        }
    }

    public class EventsImplTest
    {
        [Fact]
        public void ShouldPublish()
        {
            // Given
            var listener = new TestListener();
            var listener2 = new TestListener();
            var otherListener = new OtherTestListener();
            
            EventPublisher publisher = new EventPublisherLogic();
            publisher.AddListener(listener);
            publisher.AddListener(listener2);
            publisher.AddListener(otherListener);
            
            // When
            publisher.Publish(new TestEvent("Hello"));
            publisher.Publish(new TestEvent("World!"));
            publisher.Publish(new OtherTestEvent("Not you!"));

            // Then
            Assert.Equal(2, listener.Events.Count);
            Assert.Equal("Hello", listener.Events[0].Message);
            Assert.Equal("World!", listener.Events[1].Message);
            
            Assert.Equal(2, listener2.Events.Count);

            Assert.Equal(1, otherListener.Events.Count);
            Assert.Equal("Not you!", otherListener.Events[0].Message);
        }
    }
}
