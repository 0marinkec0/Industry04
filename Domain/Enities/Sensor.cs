using Domain.Enums;

namespace Domain.Enities
{
    public class Sensor
    {
        public Guid Id { get; set; }
        public string? DeviceId {get; set;}
        public SensorType Type { get; set; }
        public double Value { get; set; }
        public DateTime Time { get; set; }
    }
}
