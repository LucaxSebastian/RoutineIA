using RoutineAI.Domain.Entities;

namespace RoutineAI.Application.Interfaces;

public interface IAIService
{
    Task<string> GenerateMessageAsync(CalendarEvent calendarEvent);
}
