using Application.Features.Actuator.Commands.ActuatorControl.ActuatorModeChange;
using Shared.Requests.Actuator;

namespace API.MinimalAPI.ActuatorApi;

public static class ActuatorMappingExtensions
{
    public static ActuatorModeChangeCommand ToCommand(this ActuatorModeChangeRequest request, int id)
    {
        return new ActuatorModeChangeCommand
        ( 
              Id: id, 
              Mode: (Domain.Enums.Mode)request.Mode
        );
    }
}
