using MediatR;
using Shared.ResultService;

namespace Application.Features.Commands.ScheduleRule.UpdateSchedule;

public record UpdateScheduleCommand(int Id, DayOfWeek DayOfWeek, TimeOnly StartTime, TimeOnly EndTime)
    :IRequest<Result>;
