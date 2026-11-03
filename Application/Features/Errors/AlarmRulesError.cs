using Shared.ResultService;

namespace Application.Features.Errors;

public class AlarmRulesError
{
   public static Error AlarmRuleNotFound => new Error("AlarmRule.NotFound", "Alarm rule with given ID can not be found");
}
