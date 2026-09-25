using Domain.Enums;
using MediatR;

namespace Application.Features.Commands.AlarmRules.CreateRule
{
    public record AlarmRuleCreateCommand(string Name, SensorType SensorType, Operator Operator, double Threshold) : IRequest;
}
