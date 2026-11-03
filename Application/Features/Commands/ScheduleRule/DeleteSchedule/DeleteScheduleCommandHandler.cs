using Application.Features.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Shared.ResultService;

namespace Application.Features.Commands.ScheduleRule.DeleteSchedule;

public class DeleteScheduleCommandHandler : IRequestHandler<DeleteScheduleCommand, Result>
{
    private readonly IAppDbContext _appDbContext;

    public DeleteScheduleCommandHandler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Result> Handle(DeleteScheduleCommand request, CancellationToken cancellationToken)
    {
        var schedule = await _appDbContext.Schedules.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (schedule is null)
        {
            return Result.Failure(ScheduleError.ScheduleNotFound);
        }

        _appDbContext.Schedules.Remove(schedule);

        await _appDbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
