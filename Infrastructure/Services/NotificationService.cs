using Microsoft.Extensions.Options;
using RoutineAI.Application.Interfaces;
using RoutineAI.Infrastructure.Settings;
using Telegram.Bot;

namespace RoutineAI.Infrastructure.Services;

public class NotificationService(
    ITelegramBotClient botClient,
    IOptions<TelegramSettings> options
    ) : INotificationService
{
    private readonly ITelegramBotClient _botClient = botClient;
    private readonly string _chatId = options.Value.ChatId;

    public async Task SendMessageAsync(string message)
        => await _botClient.SendMessage(_chatId, message);
}
