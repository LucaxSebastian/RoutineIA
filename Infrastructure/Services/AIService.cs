using RoutineAI.Application.Interfaces;
using RoutineAI.Domain.Entities;

namespace RoutineAI.Infrastructure.Services;

public class AIService : IAIService
{
    private const string Message = "Você tem um evento em breve! Bora pra cima e nada de PROCRASTINAR!";

    public Task<string> GenerateMessageAsync(CalendarEvent calendarEvent)
        => Task.FromResult(Message);
}