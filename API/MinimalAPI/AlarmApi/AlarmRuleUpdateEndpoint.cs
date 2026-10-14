using Application.Features.Commands.AlarmRules.UpdateRule;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.MinimalAPI.AlarmApi
{
    public class AlarmRuleUpdateEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("alarm-rules/update/{id}", async (int id, [FromBody] AlarmRuleUpdateCommand command, IMediator mediator) =>
            {
                var IdRequest = command with { id = id };

                await mediator.Send(IdRequest);

                return Results.Ok();
            });
        }
    }
}
