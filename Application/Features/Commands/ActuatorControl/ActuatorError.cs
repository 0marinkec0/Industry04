using Shared.ResultService;

namespace Application.Features.Commands.ActuatorControl;

public class ActuatorError
{
    public static Error ActuatorStateError => new Error("Actuator.State", "Actuator state can only be ON or OFF");

    public static Error ActuatorIdNotFound => new Error("Actuator.Id", "Actuator with given Id can not be found");
}
