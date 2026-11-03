

namespace Shared.Respons.Schedule;

public class ScheduleResponse
{
    public int Id { get; set; }
    public int ActuatorId { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
}
