using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Respons.Schedule;
using Shared.ResultService;

namespace Application.Features.Schedules.Queries.GetSchedule;

public class GetScheduleQueryHandler : IRequestHandler<GetScheduleQuery, Result<List<ScheduleResponse>>>
{
    private readonly IAppDbContext _appDbContext;

    public GetScheduleQueryHandler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Result<List<ScheduleResponse>>> Handle(GetScheduleQuery request, CancellationToken cancellationToken)
    {
        var schedule = await _appDbContext.Schedules.Select(x => new ScheduleResponse
        {
            Id = x.Id,
            ActuatorId = x.ActuatorId,
            DayOfWeek = x.DayOfWeek,
            StartTime = x.StartTime,
            EndTime = x.EndTime
        }).ToListAsync();

        return Result<List<ScheduleResponse>>.Success(schedule);
    }
}
