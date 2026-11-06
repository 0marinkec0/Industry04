using MediatR;
using Shared.ResultService;

namespace Application.Features.Commands.ScheduleRule.UpdateSchedule;

public record UpdateScheduleCommand(int Id, int ActuatorId, DayOfWeek DayOfWeek, TimeOnly StartTime, TimeOnly EndTime)
    :IRequest<Result>;
