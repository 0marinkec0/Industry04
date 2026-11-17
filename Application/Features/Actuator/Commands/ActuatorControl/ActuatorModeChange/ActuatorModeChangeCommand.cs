using Domain.Enums;
using MediatR;
using Shared.ResultService;

namespace Application.Features.Actuator.Commands.ActuatorControl.ActuatorModeChange;

public record ActuatorModeChangeCommand(int Id, Mode Mode) : IRequest<Result>;

