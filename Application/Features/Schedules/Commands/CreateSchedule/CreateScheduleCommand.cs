using MediatR;
using Shared.ResultService;

namespace Application.Features.Schedules.Commands.CreateSchedule;

public record CreateScheduleCommand(int ActuatorId, DayOfWeek DayOfWeek, TimeOnly StartTime, TimeOnly EndTime) 
    : IRequest<Result>;

