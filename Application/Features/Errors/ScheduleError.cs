using Shared.ResultService;

namespace Application.Features.Errors;

public class ScheduleError
{
    public static Error ScheduleNotFound => new Error("Schedule.NotFound", "Schedule with given ID can not be found");
    public static Error DayOfWeekAlreadyExists => new Error("Schedule.DayOfWeek", "Can not change to already existing day");
}
