using RoutineAI.Application.Interfaces;
using RoutineAI.Domain.Entities;

namespace RoutineAI.Infrastructure.Services;

/// <summary>
/// Responsabilidade:
///     - Transformar em CalendarEvent (ou DTO)
///     - Retorna lista de eventos elegíveis para o ReminderService aplicar regras
/// </summary>
public class CalendarService : ICalendarService
{
   public Task<List<CalendarEvent>> GetEventsAsync()
    {
        var events = new List<CalendarEvent>()
        {
            new()
            {
                Title = "Treino de corrida",
                StartTime = DateTime.Now.AddMinutes(30)
            },
            new()
            {
                Title = "Estudar programação",
                StartTime = DateTime.Now.AddHours(1)
            }
        };

        return Task.FromResult(events);
    }
}