using B20.Architecture.Contexts.Api;
using B20.Architecture.Events.Fixtures;
using B20.Events.Api;

namespace B20.Tests.Architecture.Events.Context
{
    public class EventsMocks: ContextModule
    {
        public void Apply(ContextBuilder builder)
        {
            builder
                .SetImpl<EventPublisher, EventPublisherMock>();
        }
    }
}