using Application.Features.Errors;
using Application.Interfaces;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.ResultService;

namespace Application.Features.Actuator.Commands.ActuatorControl.ActuatorModeChange;

public class ActuatorModeChangeCommandHandler : IRequestHandler<ActuatorModeChangeCommand, Result>
{
    private readonly IAppDbContext _appDbContext;

    public ActuatorModeChangeCommandHandler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Result> Handle(ActuatorModeChangeCommand request, CancellationToken cancellationToken)
    {
        var actuator = await _appDbContext.Actuators.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (actuator is null)
        {
            return Result.Failure(ActuatorError.ActuatorIdNotFound);
        }

        if (actuator.Mode.Equals(Mode.Manual) && request.Mode.Equals(Mode.Automatic))
        {
            actuator.Mode = Mode.Automatic;
        }
        else if (actuator.Mode.Equals(Mode.Automatic) && request.Mode.Equals(Mode.Manual))
        {
            actuator.Mode = Mode.Manual;
        }

        await _appDbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
