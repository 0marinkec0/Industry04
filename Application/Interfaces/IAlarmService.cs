using Domain.Enities;
using Shared.ResultService;

namespace Application.Interfaces
{
    public interface IAlarmService
    {
        public Task<Result> EvaluateAlarmAsync(Sensor sensorMeasurement);
    }
}
