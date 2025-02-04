namespace AspireWebApp.Web.Components.Pages;

public class CounterState
{
    public int Count { get; private set; }

    public void IncrementCount()
    {
        Count++;
        NotifyStateChanged();
    }

    public event Action OnChange;

    private void NotifyStateChanged() => OnChange?.Invoke();

}
