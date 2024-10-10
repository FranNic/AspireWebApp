namespace MassTransit;

using MassTransitDemo;
public class PingPublisher : BackgroundService
{
    private readonly ILogger<PingPublisher> _logger;
    private readonly IBus _bus;

    public PingPublisher(ILogger<PingPublisher> logger, IBus bus)
    {
        _logger = logger;
        _bus = bus;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            //await Task.Yield();
            //if (_logger.IsEnabled(LogLevel.Information))
            //{
            //    _logger.LogInformation("PingPublisher running at: {time}", DateTimeOffset.Now);
            //    await _bus.Publish(new Ping());

            //}

            //var pressed = Console.ReadKey(true);
            //if (pressed.Key != ConsoleKey.Escape)
            //{
                await _bus.Publish(new PingRecord(($"Hello it is {DateTimeOffset.Now}")));
            //}

            await Task.Delay(1000, stoppingToken);
        }
    }
}
