namespace RoutineAI.Application.Interfaces;

public interface IAIService
{
    Task<string> GenerateMessageAsync();
}
