using Application.Features.Commands.AlarmRules.DeleteRule;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.MinimalAPI.AlarmAPI
{
    public class AlarmRuleDeleteEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("alarm-rules/delete", async ([FromBody] AlarmRuleDeleteCommand command, IMediator mediator) =>
            {

                var result = await mediator.Send(command);

                if (result.IsSuccess)
                    return Results.Ok();

                return Results.BadRequest();

            });
        }
    }
}
