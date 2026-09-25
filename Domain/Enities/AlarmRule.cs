using Domain.Enums;

namespace Domain.Enities
{
    public class AlarmRule
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public SensorType SensorType { get; set; }
        public Operator Operator { get; set; }
        public double Threshold { get; set; }
    }
}
