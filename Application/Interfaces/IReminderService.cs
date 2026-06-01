using RoutineAI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutineAI.Application.Interfaces;

public interface IReminderService
{
    Task<CalendarEvent?> GetUpcomingEventAsync();
}