using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Commands.AlarmRules.UpdateRule;

public class AlarmRuleUpdateCommandHandler : IRequestHandler<AlarmRuleUpdateCommand>
{
    private readonly IAppDbContext _appDbContext;

    public AlarmRuleUpdateCommandHandler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Unit> Handle(AlarmRuleUpdateCommand request, CancellationToken cancellationToken)
    {
        var alarmRule = await _appDbContext.AlarmRules.FirstOrDefaultAsync(x => x.Id == request.id);

        if (alarmRule is not null)
        {
            alarmRule.Name = request.Name;
            alarmRule.Operator = request.Operator;
            alarmRule.Threshold = request.Threshold;
            alarmRule.SensorType = request.SensorType;

            await _appDbContext.SaveChangesAsync();
        }

        return Unit.Value;
    }
}
