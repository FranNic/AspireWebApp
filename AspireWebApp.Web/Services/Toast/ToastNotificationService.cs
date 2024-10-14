namespace AspireWebApp.Web.Services.Toast;
public class ToastNotificationService
{
    public event Action<string, string, int> OnShow;
    public event Action<ToastMessage>? OnShoww;
    public event Action OnHide;
    public event Action<Guid>? OnHidee;
    //public void ShowToast(string message, string type = "info", int dismissAfter = 3)
    //{
    //    OnShow?.Invoke(message, type, dismissAfter);
    //}
    public void HideToast()
    {
        OnHide?.Invoke();
    }
    public void ShowSuccess(string message, int dismissAfter = 3) => ShowToast(message, "success", dismissAfter);
    public void ShowError(string message, int dismissAfter = 3) => ShowToast(message, "failure", dismissAfter);
    public void ShowWarning(string message, int dismissAfter = 3) => ShowToast(message, "warning", dismissAfter);
    public void ShowInfo(string message, int dismissAfter = 3) => ShowToast(message, "info", dismissAfter);
    public void ShowAlert(string message, int dismissAfter = 3) => ShowToast(message, "alert", dismissAfter);


    public List<ToastMessage> ToastMessages { get; set; } = new();

   

    public void ShowToast(string message, string type = "info", int dismissAfter = 3)
    {
        var toast = new ToastMessage
        {
            Id = Guid.NewGuid(),
            MessageContent = message,
            MessageType = type,
            DismissAfter = dismissAfter
        };
        ToastMessages.Add(toast);
        OnShoww?.Invoke(toast);

        Task.Delay(dismissAfter * 1000).ContinueWith(_ => RemoveToast(toast.Id));
    }

    public void RemoveToast(Guid id)
    {
        var toastToRemove = ToastMessages.FirstOrDefault(t => t.Id == id);
        if (toastToRemove != null)
        {
            ToastMessages.Remove(toastToRemove);
            OnHidee?.Invoke(id);
        }
    }

    public class ToastMessage
    {
        public Guid Id { get; set; }
        public string MessageContent { get; set; }
        public string MessageType { get; set; }
        public int DismissAfter { get; set; }
    }

}