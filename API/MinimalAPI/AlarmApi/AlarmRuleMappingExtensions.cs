using Application.Features.Commands.AlarmRules.UpdateRule;
using Shared.Requests.Alarm;

namespace API.MinimalAPI.AlarmApi;

public static class AlarmRuleMappingExtensions
{
    public static AlarmRuleUpdateCommand ToCommand(this AlarmRuleRequest request, int id)
    {
        return new AlarmRuleUpdateCommand(id: id,
            Name: request.Name,
            SensorType: (Domain.Enums.SensorType)request.SensorType,
            Operator: (Domain.Enums.Operator)request.Operator,
            Threshold: request.Threshold);  
    }
}
