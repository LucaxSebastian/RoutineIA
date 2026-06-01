using RoutineAI.Application.Interfaces;
using RoutineAI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutineAI.Infrastructure.Services;

/// <summary>
/// Responsabilidade:
///     - Buscar eventos através do ICalendarService
///     - Aplicar regras para determinar quais eventos devem gerar lembretes
///     - Retorna evento ao Worker apenas o próximo evento elegível
/// </summary>
public class ReminderService(ICalendarService calendarService) : IReminderService
{
    private readonly ICalendarService _calendarService = calendarService;

    public async Task<CalendarEvent?> GetUpcomingEventAsync()
    {
        var events = await _calendarService.GetEventsAsync();

        return events
            .Where(eventsItem =>
                eventsItem.StartTime > DateTime.Now &&
                eventsItem.StartTime <= DateTime.Now.AddHours(1))
            .OrderBy(eventsItem => eventsItem.StartTime)
            .FirstOrDefault();
    }
}