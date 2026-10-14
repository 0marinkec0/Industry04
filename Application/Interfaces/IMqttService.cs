namespace Application.Interfaces;

public interface IMqttService
{
    Task ConnectAsync(CancellationToken cancellationToken = default);
    Task SubscribeAsync(string topic, CancellationToken cancellationToken = default);
    Task PublishAsync(string topic, string payload , CancellationToken cancellationToken = default);
    Task PublishByteAsync(string topic, byte[] payload, CancellationToken cancellationToken = default);
}
