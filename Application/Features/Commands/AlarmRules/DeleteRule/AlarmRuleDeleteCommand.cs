using MediatR;
using Shared.ResultService;

namespace Application.Features.Commands.AlarmRules.DeleteRule
{
    public record AlarmRuleDeleteCommand(int ruleId) : IRequest<Result>;
}
