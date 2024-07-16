namespace AspireWebApp.Web.Components.Pages.Pomodoro;

using System.ComponentModel;
using Microsoft.AspNetCore.Components;
using System.Timers;

public class PomodoroState
{
    public PomodoroState()
    {
        InitializeTimer();
    }

    public bool IsRunning
    { get { return PomodoroTimer.Enabled; } }
    public bool IsStopped
    { get { return !IsRunning; } }
    public PomodoroTimer PomodoroTimer { get; set; }

    private double? _PomodoroTimerValue;

    public double PomodoroTimerValue
    {
        get => _PomodoroTimerValue ?? 0;
        private set
        {
            _PomodoroTimerValue = value;
            //NotifyStateChanged();
        }
    }

    public void InitializeTimer()
    {
        PomodoroTimer = new PomodoroTimer();
        PomodoroTimerValue = PomodoroTimer.TimeInSeconds;
        PomodoroTimer.Elapsed += OnTimedEvent;
    }

    private void OnTimedEvent(Object source, ElapsedEventArgs e)
    {
        PomodoroTimerValue = PomodoroTimer.DecreaseTime();

        if (PomodoroTimer.TimeInSeconds <= 0)
        {
            switch (PomodoroTimer.Status)
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

            //NotifySessionChanged();
        }
    }

    //public event Action? OnChange;
    //public event Action? OnChangedSession;

    // InvokeAsynk is used to notify the UI that the state has changed.

    //private void NotifyStateChanged() => base.InvokeAsync(OnChange);

    //private void NotifySessionChanged() => OnChangedSession?.Invoke();
}