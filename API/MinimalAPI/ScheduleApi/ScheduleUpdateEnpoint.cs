using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Requests.Schedule;

namespace API.MinimalAPI.ScheduleApi;

public class ScheduleUpdateEnpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("schedule/update/{id}", async (int id, [FromBody] ScheduleRequest request, IMediator mediator) =>
        {
            var mappedCommand = request.ToCommand(id);

            var result = await mediator.Send(mappedCommand);
            
            if(result.IsSuccess)
                return Results.Ok();

            return Results.BadRequest(result);
        });
    }
}
