using Application.Dto;
using MediatR;

namespace Application.Features.Alarm.Queries.GetAlarmRules
{
    public record AlarmRulesGetQuery : IRequest<List<AlarmRuleDTO>>;

}
