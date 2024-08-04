namespace B20.Events.Api
{
    public interface EventPublisher
    {
        void Publish<TEvent>(TEvent e) where TEvent : Event;
    }
}