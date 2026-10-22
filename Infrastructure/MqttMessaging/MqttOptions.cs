namespace Infrastructure.Messaging
{
    public sealed class MqttOptions
    {
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; }
        public MqttTopicsOptions Topics { get; set; } = new();
    }

    public class MqttTopicsOptions
    {
        public string Temperature { get; set; } = string.Empty;
        public string Humidity { get; set; } = string.Empty;
        public string ActuatorConfig { get; set; } = string.Empty;
    }
}
