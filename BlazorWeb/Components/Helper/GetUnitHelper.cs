using Shared.Enum;

namespace BlazorWeb.Components.Helper;

public class GetUnitHelper
{
    public static string GetUnit(string sensorType)
    {
        return sensorType switch
        {
            "Temperature" => "°C",
            "Humidity" => "%",
            _ => ""
        };
    }

    public static string GetUnit(SensorType sensorType)
    {
        return sensorType.ToString() switch
        {
            "Temperature" => "°C",
            "Humidity" => "%",
            _ => ""
        };
    }
}
