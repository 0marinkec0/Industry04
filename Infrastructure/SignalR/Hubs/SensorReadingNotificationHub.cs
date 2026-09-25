using Microsoft.AspNetCore.SignalR;
using Shared.Respons;

namespace Infrastructure.SignalR.Hubs
{
    public interface ISensorReadingNotificationClient
    { 
        Task SensorReadingStatusUpdate(SensorReading sensor);
    }
    public class SensorReadingNotificationHub : Hub<ISensorReadingNotificationClient>
    {
    }
}
