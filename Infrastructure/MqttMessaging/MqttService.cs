using Application.Interfaces;
using Microsoft.Extensions.Options;
using MQTTnet;

namespace Infrastructure.Messaging;
public class MqttService : IMqttService
{
    private readonly IMqttClient _mqttClient;
    private readonly MqttOptions _mqttOptions;

    public MqttService(IMqttClient mqttClient, IOptions<MqttOptions> mqttOptions)
    {
        _mqttClient = mqttClient;
        _mqttOptions = mqttOptions.Value;
    }
    public async Task ConnectAsync(CancellationToken cancellationToken = default)
    {
        if (_mqttClient.IsConnected)
            return;

        var options = new MqttClientOptionsBuilder()
            .WithTcpServer(_mqttOptions.Host, _mqttOptions.Port)
            .Build();

        await _mqttClient.ConnectAsync(options, cancellationToken);
    }
    public async Task PublishAsync(string topic, string payload, CancellationToken cancellationToken = default)
    {
        var message = new MqttApplicationMessageBuilder()
            .WithTopic(topic)
            .WithPayload(payload)
            .Build();

        await _mqttClient.PublishAsync(message, cancellationToken);
    }
    public async Task PublishByteAsync(string topic, byte[] payload, CancellationToken cancellationToken = default)
    {
        var message = new MqttApplicationMessageBuilder()
            .WithTopic(topic)
            .WithPayload(payload)
            .Build();

        await _mqttClient.PublishAsync(message, cancellationToken);
    }
    public async Task SubscribeAsync(string topic, CancellationToken cancellationToken = default)
    {
        var subscribeOptions = new MqttClientSubscribeOptionsBuilder()
            .WithTopicFilter(topic)
            .Build();

        await _mqttClient.SubscribeAsync(subscribeOptions, cancellationToken);
    }
}
