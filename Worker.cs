using RoutineAI.Application.Interfaces;

namespace RoutineAI;

public class Worker(
    ILogger<Worker> logger, 
    IServiceScopeFactory scopeFactory
    ) : BackgroundService
{
    private readonly ILogger<Worker> _logger = logger;
    private readonly IServiceScopeFactory _scopeFactory = scopeFactory;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var createScope = _scopeFactory.CreateScope();

            var calendarService = createScope.ServiceProvider.GetRequiredService<ICalendarService>();
            var aiService = createScope.ServiceProvider.GetRequiredService<IAIService>();
            var notificationService = createScope.ServiceProvider.GetRequiredService<INotificationService>();

            _logger.LogInformation("🔄 Iniciando ciclo do RoutineAI...");

            var hasEvent = await calendarService.HasEventTodayAsync();

            if (!hasEvent)
            {
                _logger.LogInformation("❌ Nenhum evento encontrado!");
                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
                continue;
            }

            _logger.LogInformation("✅ Evento encontrado");

            var isWithinOneHour = await calendarService.IsEventWithinOneHourAsync();

            if (!isWithinOneHour)
            {
                _logger.LogInformation("⏳ O evento ainda não está próximo");
                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
                continue;
            }

            _logger.LogInformation("⏰ O evento em até 1 hora!");

            var message = await aiService.GenerateMessageAsync();

            _logger.LogInformation("🤖 Mensagem gerada: {message}", message);

            await notificationService.SendMessageAsync(message);

            _logger.LogInformation("⏱️ Aguardando próxima execução...");

            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
        }
    }
}