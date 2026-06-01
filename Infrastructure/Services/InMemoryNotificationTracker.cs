using RoutineAI.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutineAI.Infrastructure.Services
{
    /// <summary>
    /// Verifica se um evento já foi notificado para evitar notificações duplicadas.
    /// </summary>
    public class InMemoryNotificationTracker : INotificationTracker
    {
        private readonly HashSet<string> _notifiedEvents = [];

        public bool WasEventNotified(string eventId)
            => _notifiedEvents.Add(eventId);

        public void MarkEventAsNotified(string eventId)
            => _notifiedEvents.Contains(eventId);
    }
}
