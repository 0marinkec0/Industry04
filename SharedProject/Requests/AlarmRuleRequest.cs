using Shared.Enum;

namespace Shared.Requests;

public class AlarmRuleRequest
{
    public string Name { get; set; }
    public SensorType SensorType { get; set; }
    public Operator Operator { get; set; }
    public double Threshold { get; set; }
}

