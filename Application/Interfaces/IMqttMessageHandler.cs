namespace Application.Interfaces;

public interface IMqttMessageHandler
{
    Task HandleAsync(string topic, string payload);
}
