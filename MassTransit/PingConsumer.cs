namespace MassTransitDemo;

using MassTransit;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

public class PingConsumer : IConsumer<PingRecord>
{
    private readonly ILogger<PingConsumer> _logger;
    private readonly IBus _bus;
    public PingConsumer(ILogger<PingConsumer> logger,IBus bus)
    {
        _logger = logger;
        _bus = bus;
    }

    public Task Consume(ConsumeContext<PingRecord> context)
    {
        if (_logger.IsEnabled(LogLevel.Information))
        {
            //_logger.LogInformation("Ping received at: {time}", DateTimeOffset.Now);
            _logger.LogInformation("Ping received: {Button}", context.Message.buttonpressed);
        }
        return Task.CompletedTask;
    }
} 