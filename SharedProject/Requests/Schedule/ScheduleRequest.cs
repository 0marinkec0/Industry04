namespace Shared.Requests.Schedule;

public class ScheduleRequest
{
    public int ActuatorId { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
}
