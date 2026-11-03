using Application.Features.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.ResultService;

namespace Application.Features.Commands.ScheduleRule.UpdateSchedule;

public class UpdateScheduleCommandHandler : IRequestHandler<UpdateScheduleCommand, Result>
{
    private readonly IAppDbContext _appDbContext;

    public UpdateScheduleCommandHandler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Result> Handle(UpdateScheduleCommand request, CancellationToken cancellationToken)
    {
        var schedule = await _appDbContext.Schedules.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (schedule is null)
        {
            return Result.Failure(ScheduleError.ScheduleNotFound);
        }
    
        var dayOfWeekAlreadyExists = await _appDbContext.Schedules
                 .AnyAsync(s =>
                  s.ActuatorId == schedule.ActuatorId &&
                  s.DayOfWeek == request.DayOfWeek &&
                  s.Id != schedule.Id);

        if (dayOfWeekAlreadyExists)
        {
            return Result.Failure(ScheduleError.DayOfWeekAlreadyExists);
        }

        schedule.DayOfWeek = request.DayOfWeek;
        schedule.StartTime = request.StartTime;
        schedule.EndTime = request.EndTime; 

        await _appDbContext.SaveChangesAsync(cancellationToken);    

        return Result.Success();
    }
}
