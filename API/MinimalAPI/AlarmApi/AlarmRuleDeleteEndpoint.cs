using Application.Features.Alarm.Commands.DeleteRule;
using Carter;
using MediatR;

namespace API.MinimalAPI.AlarmAPI;
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
