using Application.Interfaces;
using MediatR;
using Shared.ResultService;


namespace Application.Features.Commands.ActuatorControl
{
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
            var upperCaseCommand = request.status.ToUpperInvariant();

            if (upperCaseCommand is not ("ON" or "OFF"))
            {
                return Result.Failure(ActuatorError.ActuatorStateError);
            }

            await _mqttService.ConnectAsync();

            await _mqttService.PublishAsync("actuator/room-1/led", upperCaseCommand);

            return Result.Success();
        }
    }
}
