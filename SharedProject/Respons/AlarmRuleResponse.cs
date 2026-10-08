using Shared.Enum;

namespace Shared.Respons;

public class AlarmRuleResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string SensorType { get; set; }
    public string Operator { get; set; }
    public double Threshold { get; set; }
}
