using System;

namespace B20.Events.Api
{
    public interface EventListener
    {
        Type GetEventType();
        void HandleEvent(Object e);
    }
}