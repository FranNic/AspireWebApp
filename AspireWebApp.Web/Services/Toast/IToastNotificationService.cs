namespace AspireWebApp.Web.Services.Toast;

public interface IToastNotificationService
{
    Task ShowSuccess(string message, int dismissAfter = 3);
    Task ShowError(string message, int dismissAfter = 3);
    Task ShowWarning(string message, int dismissAfter = 3);
    Task ShowInfo(string message, int dismissAfter = 3);
    Task ShowAlert(string message, int dismissAfter = 3);

    Task ShowToast(string message, string type = "info", int dismissAfter = 3);

    Task RemoveToast(Guid id);

}