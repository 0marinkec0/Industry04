using MediatR;
using Shared.ResultService;

namespace Application.Features.Commands.ActuatorControl.ActuatorControl
{
    public record ActuatorControlCommand(int ActuatorId, string ActuatorCommand) : IRequest<Result>;

}
