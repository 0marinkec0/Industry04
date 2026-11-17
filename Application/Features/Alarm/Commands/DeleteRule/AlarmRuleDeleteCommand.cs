using MediatR;
using Shared.ResultService;

namespace Application.Features.Alarm.Commands.DeleteRule
{
    public record AlarmRuleDeleteCommand(int ruleId) : IRequest<Result>;
}
