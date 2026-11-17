
namespace Shared.Respons.Alarm;

public class AlarmRuleResponse
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string SensorType { get; set; }
    public required string Operator { get; set; }
    public double Threshold { get; set; }
}
