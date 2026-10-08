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
            app.MapDelete("alarm-rules/delete/{id}", async (int id, IMediator mediator) =>
            {

                var result = await mediator.Send(new AlarmRuleDeleteCommand(id));

                if (result.IsSuccess)
                    return Results.Ok();

                return Results.BadRequest();
            });
        }
    }
}
