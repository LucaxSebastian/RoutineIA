namespace RoutineAI.Infrastructure.Settings;

public class TelegramSettings
{
    public const string SectionName = "Telegram";

    public string Token { get; set; } = string.Empty;
    public string ChatId { get; set; } = string.Empty;
}