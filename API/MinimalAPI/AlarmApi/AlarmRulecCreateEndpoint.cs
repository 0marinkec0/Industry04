using Application.Features.Commands.AlarmRules.CreateRule;
using Carter;
using MediatR;

namespace API.MinimalAPI.AlarmAPI
{
    public class AlarmRulecCreateEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("alarm-rules/create", async (AlarmRuleCreateCommand command, IMediator mediator) =>
            {
                await mediator.Send(command);

                return Results.Ok();
            });
        }
    }
}
