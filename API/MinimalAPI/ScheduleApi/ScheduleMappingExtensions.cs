using Application.Features.Commands.ScheduleRule.UpdateSchedule;
using Shared.Requests.Schedule;

namespace API.MinimalAPI.ScheduleApi;

public static class ScheduleMappingExtensions
{
    public static UpdateScheduleCommand ToCommand(this ScheduleRequest request, int id)
    {
        return new UpdateScheduleCommand(
            Id: id,
            DayOfWeek: request.DayOfWeek,
            StartTime: request.StartTime,
            EndTime: request.EndTime);
    }
}
