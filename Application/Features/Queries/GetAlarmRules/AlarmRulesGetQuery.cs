using Application.Dto;
using MediatR;

namespace Application.Features.Queries.GetAlarmRules
{
    public record AlarmRulesGetQuery : IRequest<List<AlarmRuleDTO>>;

}
