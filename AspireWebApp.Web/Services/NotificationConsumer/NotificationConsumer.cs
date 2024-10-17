namespace AspireWebApp.Web.Services.NotificationConsumer;

using AspireWebApp.Web.Services.Toast;

using EventBus.Events;

using MassTransit;

public class NotificationConsumer : IConsumer<IntegrationEvent>
{
    private readonly IBus _bus;
    private readonly ToastNotificationService _toastNotificationService;
    public NotificationConsumer(IBus bus, ToastNotificationService toastNotificationService)
    {
        _bus = bus;
        _toastNotificationService = toastNotificationService;
    }

    public async Task Consume(ConsumeContext<IntegrationEvent> context)
    {
        Console.WriteLine($"Received: {context.Message.Message}");

        _ = _toastNotificationService.ShowSuccess($"Received: {context.Message.Message}");

        await Task.CompletedTask;
    }
}
