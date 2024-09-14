using B20.Architecture.Contexts.Api;
using B20.Events.Api;
using B20.Events.Impl;

namespace B20.Architecture.Events.Context
{
    public class EventsImpl: ContextModule
    {
        public void Apply(ContextBuilder builder)
        {
            builder.SetImpl<EventPublisher, EventPublisherLogic>();
        }
    }
}