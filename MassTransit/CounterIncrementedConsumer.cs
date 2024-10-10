namespace MassTransitDemo;

using AspireWebApp.Web.Services.Counter.IntegrationEvents;

using MassTransit;

using System.Threading.Tasks;

public class CounterIncrementedConsumer : IConsumer<CounterIncrementedEvent>
{
    private readonly ILogger<PingConsumer> _logger;
    private readonly IBus _bus;

    public CounterIncrementedConsumer(ILogger<PingConsumer> logger, IBus bus)
    {
        _logger = logger;
        _bus = bus;
    }

    public Task Consume(ConsumeContext<CounterIncrementedEvent> context)
    {
        if (_logger.IsEnabled(LogLevel.Information))
        {
            _logger.LogInformation("Conter incremented by {number}", context.Message.Value);
            //Console.WriteLine("Conter incremented by {number}", context.Message.Value);
        }
        return Task.CompletedTask;
    }
}