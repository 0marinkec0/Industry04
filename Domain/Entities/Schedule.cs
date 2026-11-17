namespace Domain.Entities;

public class Schedule
{
    public int Id { get; set; }
    public int ActuatorId { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }

    public Actuator Actuator { get; set; } = null!;
}
