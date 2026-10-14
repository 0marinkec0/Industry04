using Application.Interfaces;
using Domain.Enities;
using Domain.Enums;

namespace Application.Factory;

public class TemperatureSensor : ISensorFactory
{
    public string SensorTypeName => "temperature";
    public Sensor Create(string deviceId, string topic, double value)
    {
        var sensor = new Sensor
        {
            Id = Guid.NewGuid(),
            DeviceId = deviceId,
            Type = SensorType.Temperature,
            Value = value,
            Time = DateTime.Now
        };

        return sensor;
    }
}
