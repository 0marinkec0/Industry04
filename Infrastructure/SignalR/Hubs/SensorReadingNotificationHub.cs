using Domain.Enities;
using Microsoft.AspNetCore.SignalR;
using Shared.Respons;

namespace Infrastructure.SignalR.Hubs
{
    public interface ISensorReadingNotificationClient
    { 
        Task SensorReadingStatusUpdate(SensorReading sensor);

        Task AlarmNotifiactionUpdate(List<AlarmEvent> alarmEvent);
    }
    public class SensorReadingNotificationHub : Hub<ISensorReadingNotificationClient>
    {
    }
}
