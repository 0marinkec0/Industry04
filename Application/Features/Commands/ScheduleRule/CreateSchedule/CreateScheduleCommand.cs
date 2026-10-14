using MediatR;
using Shared.ResultService;

namespace Application.Features.Commands.ScheduleRule.CreateSchedule;

public record CreateScheduleCommand(int ActuatorId, DayOfWeek DayOfWeek, TimeOnly StartTime, TimeOnly EndTime) 
    : IRequest<Result>;

