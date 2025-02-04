namespace AspireWebApp.Web.Services.Counter.IntegrationEvents;

using EventBus.Events;

public record CounterDecrementedEvent : IntegrationEvent
{
    public CounterDecrementedEvent(int value)
    {
        Value = value;
    }

    public int Value { get; }
}