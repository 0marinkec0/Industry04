using Domain.Enities;


namespace Application.Interfaces;

public interface ISensorFactory
{
    string SensorTypeName { get; }
    Sensor Create(string deviceId, string topic, double value);
}
