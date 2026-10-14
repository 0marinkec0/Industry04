using Application.Factory;
using Application.Interfaces;
using Infrastructure.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Shared.Respons;


namespace Infrastructure.Messaging
{
    public class MqttMessageHandler : IMqttMessageHandler
    {
        private readonly SensorProcessor _sensorProcessor;
        private readonly IAppDbContext _iotContext;
        private readonly IAlarmService _alarmService;
        private readonly ILogger<MqttMessageHandler> _logger;
        private readonly IHubContext<SensorReadingNotificationHub, ISensorReadingNotificationClient> _hubContext;
        public MqttMessageHandler(SensorProcessor sensorProcessor, 
            ILogger<MqttMessageHandler> logger, 
            IAppDbContext iotContext,
            IAlarmService alarmService,
            IHubContext<SensorReadingNotificationHub, 
            ISensorReadingNotificationClient> hubContext)
        {
            _sensorProcessor = sensorProcessor; ;
            _logger = logger;
            _iotContext = iotContext;
            _alarmService = alarmService;
            _hubContext = hubContext;
        }
        public async Task HandleAsync(string topic, string payload)
        {
            var sensor = _sensorProcessor.CreateSensorMesurement(topic, payload);

            _logger.LogInformation($"{sensor.Id}|{sensor.Value}|{sensor.Type}");

            var sensorReading = new SensorReading
            {
                Type = sensor.Type.ToString(),
                Value = sensor.Value,
                DateTime = sensor.Time
            };

            await _hubContext.Clients.All.SensorReadingStatusUpdate(sensorReading);
             
            await _iotContext.Sensors.AddAsync(sensor);

            await _iotContext.SaveChangesAsync();

            await _alarmService.EvaluateAlarmAsync(sensor);
        }
    }
}
