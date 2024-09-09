namespace AspireWebApp.Web.Components.Pages.Pomodoro;
using System.Timers;

public class PomodoroState
{
    public PomodoroState()
    {
        InitializeTimer();
    }
    public void InitializeTimer()
    {
        if (PomodoroTimer != null)
        {
            PomodoroTimer.Dispose();
        }

        PomodoroTimer = new PomodoroTimer(1000);
        PomodoroTimer.Elapsed += UpdateTime;
        NotifyStateChanged();
    }

    private PomodoroTimer PomodoroTimer { get; set; }

    public Session SessionIn
    { get { return PomodoroTimer.SessionIn; } }

    public bool IsRunning
    { get { return PomodoroTimer.Enabled; } }

    public bool IsStopped
    { get { return !IsRunning; } }

    public double PomodoroTimeInSeconds
    {
        get => PomodoroTimer.TimeInSeconds;
    }

    public void StartTimer()
    {
        PomodoroTimer.Start();
        Console.WriteLine("Timer started");
    }

    public void StopTimer() {
        PomodoroTimer.Stop();
        Console.WriteLine("Timer stopped");
    }

    private void UpdateTime(Object source, ElapsedEventArgs e)
    {
        PomodoroTimer.DecreaseTime();
        NotifyStateChanged();

        if (PomodoroTimer.TimeInSeconds <= 0)
        {
            switch (PomodoroTimer.SessionIn)
            {
                case Session.Work:
                    PomodoroTimer.StartBreak();
                    break;

                case Session.Break:
                    if (PomodoroTimer.LongBreakInterval == 0)
                    {
                        PomodoroTimer.StartLongBreak();
                    }
                    else
                    {
                        PomodoroTimer.StartSession();
                    }
                    break;

                case Session.LongBreak:
                    PomodoroTimer.Dispose();
                    break;
            }

            NotifySessionChanged();
        }
    }

    public event Action? OnChange;
    public event Action? OnChangedSession;

    private void NotifyStateChanged() => OnChange?.Invoke();

    private void NotifySessionChanged() => OnChangedSession?.Invoke();
}