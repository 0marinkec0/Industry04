using Domain.Enums;
using MediatR;
using Shared.ResultService;

namespace Application.Features.Commands.AlarmRules.UpdateRule;

public record AlarmRuleUpdateCommand(int id,  string Name, SensorType SensorType, Operator Operator, double Threshold)
    : IRequest<Result>;

