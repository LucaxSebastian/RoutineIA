using RoutineAI.Domain.Entities;

namespace RoutineAI.Application.Interfaces;

public interface ICalendarService
{
    Task<List<CalendarEvent>> GetEventsAsync();
}
