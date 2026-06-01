using Microsoft.Extensions.Options;
using RoutineAI;
using RoutineAI.Application.Interfaces;
using RoutineAI.Infrastructure.Services;
using RoutineAI.Infrastructure.Settings;
using Telegram.Bot;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHostedService<Worker>();
builder.Services.AddScoped<ICalendarService, CalendarService>();
builder.Services.AddScoped<IAIService, AIService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IReminderService, ReminderService>();

builder.Services.AddSingleton<ITelegramBotClient>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<TelegramSettings>>().Value;
    return new TelegramBotClient(settings.Token);
});

builder.Services
    .AddOptions<TelegramSettings>()
    .Bind(builder.Configuration.GetSection(TelegramSettings.SectionName))
    .Validate(settings => !string.IsNullOrEmpty(settings.Token), "Token é obrigatório")
    .Validate(settings => !string.IsNullOrEmpty(settings.ChatId), "ChatId é obrigatório")
    .ValidateOnStart();

var host = builder.Build();
host.Run();