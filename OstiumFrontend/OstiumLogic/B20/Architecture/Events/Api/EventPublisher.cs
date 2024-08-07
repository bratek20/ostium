namespace B20.Events.Api
{
    public interface EventPublisher
    {
        void Publish<TEvent>(TEvent e) where TEvent : Event;
        
        void AddListener<TEvent>(EventListener<TEvent> listener) where TEvent : Event;
    }
}