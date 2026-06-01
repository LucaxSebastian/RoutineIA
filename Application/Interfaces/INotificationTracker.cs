using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutineAI.Application.Interfaces;

public interface INotificationTracker
{
    bool WasEventNotified(string eventId);
    void MarkEventAsNotified(string eventId);
}