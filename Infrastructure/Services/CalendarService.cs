using RoutineAI.Application.Interfaces;

namespace RoutineAI.Infrastructure.Services;

public class CalendarService : ICalendarService
{
    public Task<bool> HasEventTodayAsync()
        => Task.FromResult(true);

    public Task<bool> IsEventWithinOneHourAsync()
        => Task.FromResult(true); 
}