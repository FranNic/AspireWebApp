using EventBus.Events;

using MassTransit;
using System.Diagnostics;

    public class HelloWorldMessageConsumer : IConsumer<IntegrationEvent>
    {
        public async Task Consume(ConsumeContext<IntegrationEvent> context)
        {
            Console.WriteLine($"Received: {context.Message.Message}");
            await Task.CompletedTask;
        }
    }
