using Application.Features.Schedules.Commands.DeleteSchedule;
using Carter;
using MediatR;

namespace API.MinimalAPI.ScheduleApi;

public class ScheduleDeleteEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("schedule/delete/{id}", async (int id, IMediator mediator) =>
        {
            var result = await mediator.Send(new DeleteScheduleCommand(id));

            if (result.IsSuccess)
                return Results.Ok();

            return Results.BadRequest();
        });
    }
}
