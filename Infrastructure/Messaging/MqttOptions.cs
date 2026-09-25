namespace Infrastructure.Messaging
{
    public sealed class MqttOptions
    {
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; }
        public List<string> Topics { get; set; } = [];
    }
}
