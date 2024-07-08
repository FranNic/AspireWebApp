namespace AspireWebApp.Web.Components.Pages.Pomodoro;
using System.Timers;

public enum Session { Work, Break, LongBreak };

public class PomodoroTimer : Timer
{
    private const int Break = 5;
    private const int SessionMinutesLength = 25;
    private const int LongBreak = 30;
    public double TimeInSeconds { get; private set; }
    public Session Status { get; private set; }
    public int LongBreakInterval { get; private set; } = 4;

    public PomodoroTimer() : base(1000)
    {
        StartSession();
    }

    public void StartSession()
    {
        Status = Session.Work;
        TimeInSeconds = SessionMinutesLength * 60;
    }

    public void StartBreak()
    {
        LongBreakInterval--;
        Status = Session.Break;
        TimeInSeconds = Break * 60;
    }

    public void StartLongBreak()
    {
        LongBreakInterval = 4;
        Status = Session.LongBreak;
        TimeInSeconds = LongBreak * 60;
    }

    public double DecreaseTime() => TimeInSeconds--;
}