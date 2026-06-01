using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutineAI.Domain.Entities;

public class CalendarEvent
{
    public string Title { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }
}