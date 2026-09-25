using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Respons
{
    public class StatisticReading
    {
        public string? SensorType { get; set; }
        public double Average { get; set; }
        public double Minimal { get; set; }
        public double Maximal { get; set; }
    }
}
