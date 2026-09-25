using Shared.ResultService;

namespace API.Services
{
    public class AlarmError
    {
        public static Error AlarmRuleNotFound => new Error(code: "AlarmRules.NotFound", message: "No alarm rules found in databse!");
    }
}
