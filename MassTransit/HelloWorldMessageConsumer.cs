using EventBus.Events;

using MassTransit;

using MassTransitDemo;

using System.Diagnostics;

public class HelloWorldMessageConsumer : IConsumer<IntegrationEvent>
{

    private readonly IBus _bus;

    public HelloWorldMessageConsumer(IBus bus)
    {
        _bus = bus;
    }

    public async Task Consume(ConsumeContext<IntegrationEvent> context)
    {
        Console.WriteLine($"Received: {context.Message.Message}");
        await _bus.Publish(new PingRecord(($"Hello it is {DateTimeOffset.Now}")));

        await Task.CompletedTask;
    }
}
