namespace B20.Events.Api
{
    public interface EventPublisher
    {
        void Publish(Event e);
    }
}