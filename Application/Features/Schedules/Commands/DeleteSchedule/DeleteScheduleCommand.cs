using MediatR;
using Shared.ResultService;

namespace Application.Features.Schedules.Commands.DeleteSchedule;

public record DeleteScheduleCommand(int Id) : IRequest<Result>;


