namespace Application.Dto
{
    public class MeasurementsStatisticsDTO
    {
        public string? SensorType { get; set; }
        public double Average { get; set; }
        public double Minimal { get; set; }
        public double Maximal { get; set; }
    }
}
