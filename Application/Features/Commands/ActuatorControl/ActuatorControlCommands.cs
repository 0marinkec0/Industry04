using MediatR;
using Shared.ResultService;

namespace Application.Features.Commands.ActuatorControl
{
    public record ActuatorControlCommand(string status) : IRequest<Result>;

}
