using Application.Features.Queries.Schedule;
using Carter;
using MediatR;

namespace API.MinimalAPI.ScheduleApi;

public class GetAllScheduleEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("schedule/get-schedules", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new GetScheduleQuery());

            if (result.IsSuccess)
            { 
                return Results.Ok(result.Value);
            }

            return Results.BadRequest();
        });
    }
}
