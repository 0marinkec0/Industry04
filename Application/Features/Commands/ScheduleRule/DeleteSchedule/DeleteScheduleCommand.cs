using MediatR;
using Shared.ResultService;

namespace Application.Features.Commands.ScheduleRule.DeleteSchedule;

public record DeleteScheduleCommand(int Id) : IRequest<Result>;


