using Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MQTTnet;
using System.Text;

namespace Infrastructure.Messaging
{
    public class MqttBackgroundService : BackgroundService
    {
        private readonly IMqttService _mqttService;
        private readonly MqttOptions _options;

        private readonly IMqttClient _mqttClient;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public MqttBackgroundService(IMqttService mqttService, IOptions<MqttOptions> options, IMqttClient mqttClient, IServiceScopeFactory serviceScopeFactory)
        {
            _mqttService = mqttService;
            _options = options.Value;
            _mqttClient = mqttClient;
            _serviceScopeFactory = serviceScopeFactory;
        }
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            _mqttClient.ApplicationMessageReceivedAsync += OnMessageReceivedAsync;

            await _mqttService.ConnectAsync(cancellationToken);

            foreach (var topic in _options.Topics)
            {
                await _mqttService.SubscribeAsync(topic, cancellationToken);
            }
          
            await Task.Delay( Timeout.Infinite, cancellationToken);
        }

        private async Task OnMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs e)
        {
            var topic = e.ApplicationMessage.Topic;

            var payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);

            using var scope = _serviceScopeFactory.CreateScope();

            var handler = scope.ServiceProvider
                .GetRequiredService<IMqttMessageHandler>();

            await handler.HandleAsync(topic, payload);
        }
    }
}
