using Application.Features.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.ResultService;

namespace Application.Features.Schedules.Commands.CreateSchedule;

public class CreateScheduleCommandHandler : IRequestHandler<CreateScheduleCommand, Result>
{
    private readonly IAppDbContext _appDbContext;

    public CreateScheduleCommandHandler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Result> Handle(CreateScheduleCommand request, CancellationToken cancellationToken)
    {
        var actuator = await _appDbContext.Actuators
          .FirstOrDefaultAsync(x => x.Id == request.ActuatorId);

        if (actuator == null)
        {
            return Result.Failure(ActuatorError.ActuatorIdNotFound);
        }

        var schedule = new Domain.Entities.Schedule
        {
            ActuatorId = request.ActuatorId,
            DayOfWeek = request.DayOfWeek,
            StartTime = request.StartTime,
            EndTime = request.EndTime,
        };

        await _appDbContext.Schedules.AddAsync(schedule);

        await _appDbContext.SaveChangesAsync();

        return Result.Success();
    }
}
