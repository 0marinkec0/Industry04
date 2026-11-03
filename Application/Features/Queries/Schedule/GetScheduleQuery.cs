using MediatR;
using Shared.Respons.Schedule;
using Shared.ResultService;

namespace Application.Features.Queries.Schedule;

public record GetScheduleQuery : IRequest<Result<List<ScheduleResponse>>>;

