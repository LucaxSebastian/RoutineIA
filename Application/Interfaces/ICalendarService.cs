namespace RoutineAI.Application.Interfaces;

public interface ICalendarService
{
    Task<bool> HasEventTodayAsync();
    Task<bool> IsEventWithinOneHourAsync();
}
