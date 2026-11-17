using MediatR;
using Shared.ResultService;

namespace Application.Features.Actuator.Commands.ActuatorControl.ActuatorControl
{
    public record ActuatorControlCommand(int ActuatorId, string ActuatorCommand) : IRequest<Result>;

}
