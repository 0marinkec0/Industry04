using Application.Features.Commands.ActuatorControl;
using Carter;
using MediatR;


namespace API.MinimalAPI.ActuatorAPI
{
    public class ActuatorControlEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("actuator/control", async (string state, IMediator mediator) => 
            {
                var result = await mediator.Send(new ActuatorControlCommand(state));

                return Results.Ok(result);
            });
        }
    }
}
