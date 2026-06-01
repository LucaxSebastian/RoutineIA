using RoutineAI.Application.Interfaces;
using System.Linq;

namespace RoutineAI;

/// <summary>
/// Responsabilidades:
///     - Orquestar o ReminderService para buscar o próximo evento elegível
///     - Orquestar o AIService para gerar mensagem
///     - Orquestar o NotificationService para enviar mensagem
///     - Controla o ciclo de execução da aplicação
/// </summary>
/// <param name="logger"></param>
/// <param name="scopeFactory"></param>
public class Worker(ILogger<Worker> logger,  IServiceScopeFactory scopeFactory) : BackgroundService
{
    private readonly ILogger<Worker> _logger = logger;
    private readonly IServiceScopeFactory _scopeFactory = scopeFactory;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var createScope = _scopeFactory.CreateScope();

            var aiService = createScope.ServiceProvider.GetRequiredService<IAIService>();
            var notificationService = createScope.ServiceProvider.GetRequiredService<INotificationService>();
            var reminderService = createScope.ServiceProvider.GetRequiredService<IReminderService>();
            var tracker = createScope.ServiceProvider.GetRequiredService<INotificationTrackerService>();

            _logger.LogInformation("Iniciando ciclo do RoutineAI...");

            var upcomingEvent = await reminderService.GetUpcomingEventAsync();

            if (upcomingEvent is null)
            {
                _logger.LogInformation("Nenhum evento próximo encontrado");
                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
                continue;
            }

            var eventId = upcomingEvent.Id.ToString();

            var alreadyNotified = tracker.WasEventNotified(eventId);

            _logger.LogInformation("Evento {Id} já foi notificado? {Result}", eventId, alreadyNotified);

            if (alreadyNotified)
            {
                _logger.LogInformation("Igorando evento '{Title}' pois ele já foi notificado", upcomingEvent.Title);
                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
                continue;
            }

            _logger.LogInformation("Evento próximo encontrado: {Title}", upcomingEvent.Title);

            var message = await aiService.GenerateMessageAsync(upcomingEvent);

            _logger.LogInformation("Mensagem gerada: {Message}", message);

            await notificationService.SendMessageAsync(message);

            tracker.MarkEventAsNotified(eventId);

            _logger.LogInformation("Aguardando próxima execução...");

            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
        }
    }
}