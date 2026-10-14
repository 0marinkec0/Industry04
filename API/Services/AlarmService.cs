using Application.Interfaces;
using Domain.Enities;
using Domain.Enums;
using Infrastructure.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Shared.ResultService;


namespace API.Services
{
    public class AlarmService : IAlarmService
    {
        private readonly IAppDbContext _iotContext;
        private readonly ILogger<AlarmService> _logger;
        private readonly IHubContext<SensorReadingNotificationHub, ISensorReadingNotificationClient> _hubContext;
        public AlarmService(IAppDbContext iotContext, ILogger<AlarmService> logger, IHubContext<SensorReadingNotificationHub, ISensorReadingNotificationClient> hubContext)
        {
            _iotContext = iotContext;
            _logger = logger;
            _hubContext = hubContext;
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
                var checkAlarmTriggered = EvaluateAlarmBasedOnOperation(rule, sensorMeasurement.Value);

                if (checkAlarmTriggered.IsTriggered)
                {
                    await TriggerAlarmAsync(rule, sensorMeasurement, checkAlarmTriggered.Operator);

                    var alarmEvents = await _iotContext.AlarmEvents.AsNoTracking().ToListAsync();

                    await _hubContext.Clients.All.AlarmNotifiactionUpdate(alarmEvents);
                }
                
                if(!checkAlarmTriggered.IsTriggered && getAlarmRulesFromDatabase.Any())
                {
                    await ResolveAlarmAsync( sensorMeasurement);
                }
            }

            return Result.Success();
        }
        private (bool IsTriggered, Operator Operator) EvaluateAlarmBasedOnOperation(AlarmRule rule, double value)
        => (
                 rule.Operator switch
                 {
                      Operator.Greater => value > rule.Threshold,
                      Operator.Less => value < rule.Threshold,
                      Operator.GreaterOrEqual => value >= rule.Threshold,
                      Operator.LessOrEqual => value <= rule.Threshold,
                      Operator.Equal => value == rule.Threshold,
                      _ => false
                 },
                 rule.Operator
         );

        private async Task TriggerAlarmAsync(AlarmRule rule, Sensor sensor, Operator operation)
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
                TriggerValue = sensor.Value,
                SensorType = sensor.Type.ToString(),
                Description = $"{sensor.Type.ToString()} in {sensor.DeviceId} is {operation.ToString()} {sensor.Value}"
            };

            await _iotContext.AlarmEvents.AddAsync(alarm);

            await _iotContext.SaveChangesAsync();
        }

        private async Task ResolveAlarmAsync(Sensor sensor)
        {
            var activeAlarm = await _iotContext.AlarmEvents
                .FirstOrDefaultAsync(x => x.DeviceId == sensor.DeviceId && !x.IsResolved);

            if (activeAlarm == null)
                return;

            activeAlarm.IsResolved = true;
            activeAlarm.ResolvedAt = DateTime.UtcNow;

            await _iotContext.SaveChangesAsync();
        }
    }
}
