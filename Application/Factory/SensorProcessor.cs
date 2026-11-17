using Application.Interfaces;
using Domain.Entities;
using System.Globalization;

namespace Application.Factory;

public class SensorProcessor
{
    private readonly Dictionary<string, ISensorFactory> _sensorFactory;

    public SensorProcessor(IEnumerable<ISensorFactory> sensorFactory)
    {
        _sensorFactory = sensorFactory.ToDictionary(s => s.SensorTypeName, s => s);    
    }

    public Sensor CreateSensorMesurement(string topic, string payload)
    {
        var sensorType = topic.Split("/").Last();

        var deviceId = topic.Split("/")[^2];

        double value = double.Parse(payload, CultureInfo.InvariantCulture);


        if (!_sensorFactory.TryGetValue(sensorType, out var sensorFactory))
        {
            throw new ArgumentException("Unknow sensor type", nameof(sensorType));
        }

        return sensorFactory.Create(deviceId, topic, value);
    }
}
