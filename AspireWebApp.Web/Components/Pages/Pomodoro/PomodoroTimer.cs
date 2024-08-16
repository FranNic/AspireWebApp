namespace AspireWebApp.Web.Components.Pages.Pomodoro;

using System.Timers;

public enum Session
{ Work, Break, LongBreak };

public class PomodoroTimer : Timer
{
    private const int Break = 5;
    private const int SessionMinutesLength = 25;
    private const int LongBreak = 30;
    public double TimeInSeconds { get; set; }
    public Session SessionIn { get; private set; }
    public int LongBreakInterval { get; private set; } = 4;

    public PomodoroTimer(int _interval) : base(_interval)
    {
        StartSession();
    }

    public void StartSession()
    {
        SessionIn = Session.Work;
        TimeInSeconds = SessionMinutesLength * 60;
    }

    public void StartBreak()
    {
        LongBreakInterval--;
        SessionIn = Session.Break;
        TimeInSeconds = Break * 60;
    }

    public void StartLongBreak()
    {
        LongBreakInterval = 4;
        SessionIn = Session.LongBreak;
        TimeInSeconds = LongBreak * 60;
    }

    public double DecreaseTime() => TimeInSeconds -= 1; 
}