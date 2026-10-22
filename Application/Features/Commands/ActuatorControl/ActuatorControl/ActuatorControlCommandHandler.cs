using Application.Interfaces;
using Domain.Enums;
using MediatR;
using Shared.ResultService;


namespace Application.Features.Commands.ActuatorControl.ActuatorControl;

public class ActuatorControlCommandHandler : IRequestHandler<ActuatorControlCommand, Result>
{
    private readonly IMqttService _mqttService;
    private readonly IAppDbContext _iotContext;

    public ActuatorControlCommandHandler(IAppDbContext iotContext, IMqttService mqttService)
    {
        _iotContext = iotContext;
        _mqttService = mqttService;
    }
    public async Task<Result> Handle(ActuatorControlCommand request, CancellationToken cancellationToken)
    {
        string upperCaseCommand = request.ActuatorCommand.ToUpperInvariant();

        if (upperCaseCommand is not ("ON" or "OFF"))
        {
            return Result.Failure(ActuatorError.ActuatorStateError);
        }

        byte parsedCommand = TryParseStringToEnum(upperCaseCommand);

        if (!Enum.IsDefined(typeof(ActuatorId), request.ActuatorId))
        {
            return Result.Failure(ActuatorError.ActuatorIdNotFound);
        }
       
        byte parseId = (byte)(ActuatorId)request.ActuatorId;

        byte[] payload = { parseId,  parsedCommand };

        await _mqttService.ConnectAsync();

        await _mqttService.PublishByteAsync("actuator/room-1/led", payload);

        return Result.Success();
    }

    private static byte TryParseStringToEnum(string value)
    {
        ActuatorCommand command;

        switch (value)
        {
            case "ON":
                command = ActuatorCommand.ON;
                return (byte)command;

            case "OFF":
                command = ActuatorCommand.OFF;
                return (byte)command;

            default:
                command = default;
                return (byte)command;
        }
    }
}
