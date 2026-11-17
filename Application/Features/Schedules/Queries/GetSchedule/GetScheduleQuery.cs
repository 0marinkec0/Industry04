using MediatR;
using Shared.Respons.Schedule;
using Shared.ResultService;

namespace Application.Features.Schedules.Queries.GetSchedule;

public record GetScheduleQuery : IRequest<Result<List<ScheduleResponse>>>;

