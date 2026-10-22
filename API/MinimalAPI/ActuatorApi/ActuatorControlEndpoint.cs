using Application.Features.Commands.ActuatorControl.ActuatorControl;
using Carter;
using MediatR;


namespace API.MinimalAPI.ActuatorAPI
{
    public class ActuatorControlEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("actuator/control", async (int id, string state, IMediator mediator) => 
            {       
                var result = await mediator.Send(new ActuatorControlCommand(id, state));

                return Results.Ok(result);
            });
        }
    }
}
