using Microsoft.AspNetCore.SignalR.Client;
using Shared.Respons.Alarm;
using Shared.Respons.Sensor;

namespace BlazorWeb.Services;

public class HubService : IAsyncDisposable
{
    private readonly HubConnection _connection;
    public event Action<SensorReading>? SensorReadingUpdate;
    public event Action<List<AlarmNotificationResponse>>? AlarmNotificationResponseUpdate;
    public HubService()
    {
        _connection = new HubConnectionBuilder()
            .WithUrl("https://localhost:7244/sensor-readings-hub")
            .WithAutomaticReconnect()
            .Build();

        _connection.On<SensorReading>("SensorReadingStatusUpdate",
            sensor =>
            {
                Console.WriteLine($"RECEIVED SENSOR: {sensor.Type} = {sensor.Value}");

                SensorReadingUpdate?.Invoke(sensor);
            });

        _connection.On<List<AlarmNotificationResponse>>("AlarmNotifiactionUpdate",
            alarm =>
            {
                foreach (var a in alarm) { 
                Console.WriteLine($"RECEIVED ALARM: {a.SensorType} trigger value {a.TriggerValue}");
            }

                AlarmNotificationResponseUpdate?.Invoke(alarm);
            });
    }
    public async Task StartAsync()
    {
        if (_connection.State == HubConnectionState.Disconnected)
        {
            Console.WriteLine($"SignalR state: {_connection.State}");
            Console.WriteLine($"SignalR URL: {_connection}");

            await _connection.StartAsync();
        }
    }
    public async ValueTask DisposeAsync()
    {
        await _connection.DisposeAsync();
    }
}
