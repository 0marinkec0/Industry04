using Domain.Enums;
using MediatR;

namespace Application.Features.Alarm.Commands.CreateRule
{
    public record AlarmRuleCreateCommand(string Name, SensorType SensorType, Operator Operator, double Threshold) : IRequest;
}
