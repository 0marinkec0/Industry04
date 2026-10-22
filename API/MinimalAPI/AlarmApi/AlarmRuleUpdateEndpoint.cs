using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Requests.Alarm;

namespace API.MinimalAPI.AlarmApi;

public class AlarmRuleUpdateEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("alarm-rules/update/{id}", async (int id, [FromBody] AlarmRuleRequest request, IMediator mediator) =>
        {
            var mappedCommand = request.ToCommand(id);

            var result = await mediator.Send(mappedCommand);

            if(result.IsSuccess)
                return Results.Ok();

            return Results.BadRequest(result);
        });
    }
}
