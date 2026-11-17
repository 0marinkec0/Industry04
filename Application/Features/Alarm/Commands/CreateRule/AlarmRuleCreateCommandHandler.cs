using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.Alarm.Commands.CreateRule;

public class AlarmRuleCreateCommandHandler : IRequestHandler<AlarmRuleCreateCommand>
{
    private readonly IAppDbContext _iotContext;

    public AlarmRuleCreateCommandHandler(IAppDbContext iotContext)
    {
        _iotContext = iotContext;
    }

    public async Task Handle(AlarmRuleCreateCommand request, CancellationToken cancellationToken)
    {
        var alarmRule = new AlarmRule
        {
            Name = request.Name,
            SensorType = request.SensorType,
            Operator = request.Operator,
            Threshold = request.Threshold,
        };

        await _iotContext.AlarmRules.AddAsync(alarmRule);

        await _iotContext.SaveChangesAsync();
    }
}
