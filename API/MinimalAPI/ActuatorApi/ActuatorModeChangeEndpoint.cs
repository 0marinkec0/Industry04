using AutoMapper;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Requests.Actuator;

namespace API.MinimalAPI.ActuatorApi;

public class ActuatorModeChangeEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("actuator/mode-change/{id}", async (int id, [FromBody]ActuatorModeChangeRequest request, IMediator mediator, IMapper mapper) =>
        {
            var mappedCommand = request.ToCommand(id);

            var result = await mediator.Send(mappedCommand);

            if (result.IsSuccess)
                return Results.Ok();

            return Results.BadRequest(result);
        });
    }
}
