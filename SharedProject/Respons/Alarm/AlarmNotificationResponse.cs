using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Respons.Alarm
{
    public class AlarmNotificationResponse
    {
        public string? DeviceId { get; set; }
        public double TriggerValue { get; set; }
        public DateTime TriggerTime { get; set; }
        public bool IsResolved { get; set; }
        public string? SensorType { get; set; }
        public string? Description { get; set; }
    }
}
