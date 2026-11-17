using Domain.Enums;

namespace Domain.Entities;

public class Actuator
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public byte ByteId { get; set; }
    public Mode Mode { get; set; }

    public List<Schedule> Schedules { get; set; } = new List<Schedule>();
}
