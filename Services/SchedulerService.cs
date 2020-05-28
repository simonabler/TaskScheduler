using System;
using System.Collections.Generic;
using System.Threading;

public class SchedulerService
{
    private static SchedulerService _instance;
    public static SchedulerService Instance => _instance ?? (_instance = new SchedulerService());

    private readonly Dictionary<int, Timer> _timers = new Dictionary<int, Timer>();

    private SchedulerService() { }
    
    public int ScheduleTask(int hour, int min, double intervalInHour, Action task)
    {
        DateTime now = DateTime.Now;
        DateTime firstRun = new DateTime(now.Year, now.Month, now.Day, hour, min, 0, 0);
        if (now > firstRun)
        {
            firstRun = firstRun.AddDays(1);
        }

        TimeSpan timeToGo = firstRun - now;
        if (timeToGo <= TimeSpan.Zero)
        {
            timeToGo = TimeSpan.Zero;
        }

        return ScheduleTask(timeToGo, intervalInHour, task);
    }

    public int ScheduleTask(double intervalInHour, Action task)
    {
        return ScheduleTask(TimeSpan.Zero, intervalInHour, task);
    }

    public int ScheduleTask(TimeSpan timeToGo, double intervalInHour, Action task)
    {
        var timer = new Timer(x =>
        {
            task.Invoke();
        }, null, timeToGo, TimeSpan.FromHours(intervalInHour));
        return AddTimer(timer);
    }

    public void Stop()
    {
        foreach (var timer in _timers.Values)
        {
            StopTimer(timer);
        }
        _timers.Clear();
    }

    public void Stop(int index)
    {
        if (!_timers.ContainsKey(index))
            return;

        StopTimer(_timers[index]);
        _timers.Remove(index);
    }

    private void StopTimer(Timer timer)
    {
        timer.Change(0, Timeout.Infinite);
        timer.Dispose();
    }

    private int AddTimer(Timer timer)
    {
        int hash = RandomNumber(int.MinValue, int.MaxValue);

        while (_timers.ContainsKey(hash))
        {
            hash = RandomNumber(int.MinValue, int.MaxValue);
        }

        _timers.Add(hash, timer);
        return hash;
    }

    private int RandomNumber(int min, int max)
    {
        Random random = new Random();
        return random.Next(min, max);
    }
}