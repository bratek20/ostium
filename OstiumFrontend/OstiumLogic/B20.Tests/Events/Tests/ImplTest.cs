using System;
using System.Collections.Generic;
using B20.Events.Api;
using B20.Logic.Events.Impl;
using B20.Logic.Utils;
using Xunit;

namespace B20.Tests.Events.Tests
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

    public class TestListener : EventListener
    {
        public List<TestEvent> Events { get; }

        public TestListener()
        {
            Events = new List<TestEvent>();
        }

        public Type GetEventType()
        {
            return typeof(TestEvent);
        }

        public void HandleEvent(object e)
        {
            Events.Add((TestEvent)e);
        }
    }

    public class OtherTestListener : EventListener
    {
        public List<OtherTestEvent> Events { get; }

        public OtherTestListener()
        {
            Events = new List<OtherTestEvent>();
        }

        public void HandleEvent(object e)
        {
            Events.Add((OtherTestEvent)e);
        }

        public Type GetEventType()
        {
            return typeof(OtherTestEvent);
        }
    }

    public class EventsImplTest
    {
        [Fact]
        public void ShouldPublish()
        {
            // Given
            var listener = new TestListener();
            var otherListener = new OtherTestListener();

            EventPublisher publisher = new EventPublisherLogic(
                ListUtils.ListOf<EventListener>(
                    listener,
                    otherListener
                )
            );

            // When
            publisher.Publish(new TestEvent("Hello"));
            publisher.Publish(new TestEvent("World!"));
            publisher.Publish(new OtherTestEvent("Not you!"));

            // Then
            Assert.Equal(2, listener.Events.Count);
            Assert.Equal("Hello", listener.Events[0].Message);
            Assert.Equal("World!", listener.Events[1].Message);

            Assert.Equal(1, otherListener.Events.Count);
            Assert.Equal("Not you!", otherListener.Events[0].Message);
        }
    }
}
