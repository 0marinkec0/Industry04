using Application.Features.Actuator.Queries.GetActuator;
using Carter;
using MediatR;

namespace API.MinimalAPI.ActuatorApi;

public class GetAllActuatorApi : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("actuator/get-actuators", async (IMediator mediator) =>
        {
            var response = await mediator.Send(new GetActuatorQuery());

            if(response.IsSuccess)
                return Results.Ok(response.Value);

            return Results.BadRequest(response);
        });
    }
}
