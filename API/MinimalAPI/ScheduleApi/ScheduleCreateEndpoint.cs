using Application.Features.Schedules.Commands.CreateSchedule;
using Carter;
using MediatR;

namespace API.MinimalAPI.ScheduleApi;

public class ScheduleCreateEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("schedule/create", async (CreateScheduleCommand command,IMediator mediator) =>
        {
            var result = await mediator.Send(command);

            if (result.IsSuccess)
                return Results.Ok();

            return Results.BadRequest();    
        });
    }
}
