using Application.Dto;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Queries.GetAlarmRules
{
    public class AlarmRulesGetQueryHandler : IRequestHandler<AlarmRulesGetQuery, List<AlarmRuleDTO>>
    {
        private readonly IAppDbContext _iotContext;

        public AlarmRulesGetQueryHandler(IAppDbContext iotContext)
        {
            _iotContext = iotContext;
        }

        public async Task<List<AlarmRuleDTO>> Handle(AlarmRulesGetQuery request, CancellationToken cancellationToken)
        {
            var result = await _iotContext.AlarmRules.Select(x => new AlarmRuleDTO
            {
                Id = x.Id,
                SensorType = x.SensorType.ToString(),
                Name = x.Name,
                Operator = x.Operator.ToString(),
                Threshold = x.Threshold,
            }).ToListAsync();

            return result;
        }
    }
}
