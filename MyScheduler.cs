using System;

public static class MyScheduler
{
    public static int IntervalInSeconds(int hour, int sec, double interval, Action task)
    {
        interval /= 3600;
        return SchedulerService.Instance.ScheduleTask(hour, sec, interval, task);
    }

    public static int IntervalInSeconds(double interval, Action task)
    {
        interval /= 3600;
        return SchedulerService.Instance.ScheduleTask(interval, task);
    }

    public static int IntervalInMinutes(int hour, int min, double interval, Action task)
    {
        interval /= 60;
        return SchedulerService.Instance.ScheduleTask(hour, min, interval, task);
    }

    public static int IntervalInMinutes(double interval, Action task)
    {
        interval /= 60;
        return SchedulerService.Instance.ScheduleTask(interval, task);
    }

    public static int IntervalInHours(int hour, int min, double interval, Action task)
    {
        return SchedulerService.Instance.ScheduleTask(hour, min, interval, task);
    }
   
    public static int IntervalInHours(double interval, Action task)
    {
        return SchedulerService.Instance.ScheduleTask(interval, task);
    }

    public static int IntervalInDays(int hour, int min, double interval, Action task)
    {
        interval *= 24;
        return SchedulerService.Instance.ScheduleTask(hour, min, interval, task);
    }

    public static int IntervalInDays(double interval, Action task)
    {
        interval *= 24;
        return SchedulerService.Instance.ScheduleTask(interval, task);
    }

    public static void Stop()
    {
        SchedulerService.Instance.Stop();
    }

    public static void Stop(int timerIndex)
    {
        SchedulerService.Instance.Stop(timerIndex);
    }
}