namespace RoutineAI.Application.Interfaces;

public interface INotificationService
{
    Task SendMessageAsync(string message);
}