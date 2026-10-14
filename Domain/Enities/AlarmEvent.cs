namespace Domain.Enities
{
    public class AlarmEvent
    {
        public int AlarmId { get; set; }
        public string? DeviceId { get; set; }
        public double TriggerValue { get; set; }
        public DateTime TriggerTime { get; set; }
        public DateTime? ResolvedAt { get; set; }
        public bool IsResolved { get; set; }
        public string? SensorType { get; set; }
        public string? Description { get; set; }
    }
}
