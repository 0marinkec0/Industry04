using Domain.Enums;
using MediatR;
using Shared.ResultService;

namespace Application.Features.Alarm.Commands.UpdateRule;

public record AlarmRuleUpdateCommand(int id,  string Name, SensorType SensorType, Operator Operator, double Threshold)
    : IRequest<Result>;

