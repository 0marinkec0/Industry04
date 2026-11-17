using Application.Features.Alarm.Queries.GetAlarmRules;
using Carter;
using MediatR;

namespace API.MinimalAPI.AlarmApi;
public class GetAlarmRuleEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("alarm-rules/get-alarm-rules", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new AlarmRulesGetQuery(), default);

            if (result != null)
                return Results.Ok(result);

            return Results.BadRequest();
        });
    }
}
