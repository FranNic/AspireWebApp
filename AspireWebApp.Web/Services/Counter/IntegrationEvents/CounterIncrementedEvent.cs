namespace AspireWebApp.Web.Services.Counter.IntegrationEvents;

using EventBus.Events;

public record CounterIncrementedEvent : IntegrationEvent
{
    public CounterIncrementedEvent(int value)
    {
        Value = value;
    }

    public int Value { get; }
}
