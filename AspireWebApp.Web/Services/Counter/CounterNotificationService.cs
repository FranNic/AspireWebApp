namespace AspireWebApp.Web.Services.Counter;

using AspireWebApp.Web.Services.Counter.IntegrationEvents;

using MassTransit;

public class CounterNotificationService : ICounterNotificationService
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IBus _bus;

    public CounterNotificationService(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    // This class is used to notify the application when the counter changes.
    public async Task SendCounterIncrementedEvent()
    {
        await _publishEndpoint.Publish(new CounterIncrementedEvent(1));
    }
}

public interface ICounterNotificationService
{
    Task SendCounterIncrementedEvent();
}