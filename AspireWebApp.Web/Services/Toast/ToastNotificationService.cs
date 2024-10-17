namespace AspireWebApp.Web.Services.Toast;

public class ToastNotificationService : IToastNotificationService
{
    public List<ToastMessage> ToastMessages { get; set; } = new();

    public event Action<ToastMessage>? OnShow;

    public event Action<Guid>? OnHide;

    public Task HideToast(Guid id)
    {
        OnHide?.Invoke(id);
        return Task.CompletedTask;
    }

    public Task ShowSuccess(string message, int dismissAfter = 3) => ShowToast(message, "success", dismissAfter);

    public Task ShowError(string message, int dismissAfter = 3) => ShowToast(message, "failure", dismissAfter);

    public Task ShowWarning(string message, int dismissAfter = 3) => ShowToast(message, "warning", dismissAfter);

    public Task ShowInfo(string message, int dismissAfter = 3) => ShowToast(message, "info", dismissAfter);

    public Task ShowAlert(string message, int dismissAfter = 3) => ShowToast(message, "alert", dismissAfter);

    public Task ShowToast(string message, string type = "info", int dismissAfter = 3)
    {
        var toast = new ToastMessage(message, type, dismissAfter);
        ToastMessages.Add(toast);
        OnShow?.Invoke(toast);

        Task.Delay(dismissAfter * 1000).ContinueWith(_ => RemoveToast(toast.Id));

        return Task.CompletedTask;
    }

    public Task RemoveToast(Guid id)
    {
        var toastToRemove = ToastMessages.FirstOrDefault(t => t.Id == id);
        if (toastToRemove != null)
        {
            ToastMessages.Remove(toastToRemove);
            OnHide?.Invoke(id);
        }
        return Task.CompletedTask;
    }

    public record ToastMessage
    {
        public ToastMessage(string messageContent, string messageType, int dismissAfter)
        {
            Id = Guid.NewGuid();
            MessageContent = messageContent;
            MessageType = messageType;
            DismissAfter = dismissAfter;
        }

        public Guid Id { init; get; }
        public string MessageContent { get; set; }
        public string MessageType { get; set; }
        public int DismissAfter { get; set; }

        public string MessageTypeClass => MessageType switch
        {
            "success" => "toast-message-success",
            "failure" => "toast-message-failure",
            "alert" => "toast-message-alert",
            "warning" => "toast-message-warning",
            _ => "toast-message-default"
        };
    }
}