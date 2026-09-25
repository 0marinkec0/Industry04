using Application.Interfaces;
using Domain.Enities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Shared.ResultService;

namespace API.Services
{
    public class AlarmService : IAlarmService
    {
        private readonly IAppDbContext _iotContext;
        private readonly ILogger<AlarmService> _logger;
        public AlarmService(IAppDbContext iotContext, ILogger<AlarmService> logger)
        {
            _iotContext = iotContext;
            _logger = logger;
        }

        public async Task<Result> EvaluateAlarmAsync(Sensor sensorMeasurement)
        {
            var getAlarmRulesFromDatabase = await _iotContext.AlarmRules
                .Where(x=>x.SensorType == sensorMeasurement.Type)
                .ToListAsync();

            if (!getAlarmRulesFromDatabase.Any())
            {
                return Result.Failure(AlarmError.AlarmRuleNotFound);
            }


            foreach (var rule in getAlarmRulesFromDatabase)
            {
                var checkAlarmTriggeredCondition = EvaluateAlarmBasedOnOpearation(rule, sensorMeasurement.Value);

                if (checkAlarmTriggeredCondition)
                {
                    await TriggerAlarmAsync(rule, sensorMeasurement);
                }
                
                if(!checkAlarmTriggeredCondition && getAlarmRulesFromDatabase.Any())
                {
                    await ResolveAlarmAsync( sensorMeasurement);
                }
            }

            return Result.Success();
        }


        private bool EvaluateAlarmBasedOnOpearation(AlarmRule rule, double value)
            => rule.Operator switch
            {
                Operator.Greater => value > rule.Threshold,
                Operator.Less => value < rule.Threshold,
                Operator.GreaterOrEqual => value >= rule.Threshold,
                Operator.LessOrEqual  => value <= rule.Threshold,
                Operator.Equal => value == rule.Threshold,
                _ => false
            };


        private async Task TriggerAlarmAsync(AlarmRule rule, Sensor sensor)
        {
            var alreadyActiveAlarm = await _iotContext.AlarmEvents
                .AnyAsync(x => x.DeviceId == sensor.DeviceId && !x.IsResolved);

            if (alreadyActiveAlarm)
            {
                return;
            }

            var alarm = new AlarmEvent
            {
                DeviceId = sensor.DeviceId,
                IsResolved = false,
                TriggerTime = DateTime.UtcNow,
                TriggerVlaue = sensor.Value,
                SensorType = sensor.Type.ToString()
            };

            await _iotContext.AlarmEvents.AddAsync(alarm);

            await _iotContext.SaveChangesAsync();
        }

        private async Task ResolveAlarmAsync(Sensor sensor)
        {
            var activeAlarm = await _iotContext.AlarmEvents
                .FirstOrDefaultAsync(x =>
                    x.DeviceId == sensor.DeviceId &&
                    !x.IsResolved);

            if (activeAlarm == null)
                return;

            activeAlarm.IsResolved = true;
            activeAlarm.ResolvedAt = DateTime.UtcNow;

            await _iotContext.SaveChangesAsync();
        }
    }
}
