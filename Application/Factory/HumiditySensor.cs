using Application.Interfaces;
using Domain.Enities;
using Domain.Enums;

namespace Application.Factory
{
    public class HumiditySensor : ISensorFactory
    {
        public string SensorTypeName => "humidity";

        public Sensor Create(string deviceId, string topic, double value)
        {
            var sensor = new Sensor
            {
                Id = Guid.NewGuid(),
                DeviceId = deviceId,
                Type = SensorType.Humidity,
                Value = value,
                Time = DateTime.Now
            };

            return sensor;
        }
    }
}
