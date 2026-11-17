using Application.Features.Sensor.Queries.GetSensorReadingStatistic;
using Carter;
using MediatR;

namespace API.MinimalAPI.SensorAPI;

public class GetSensorStatisticEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("sensor/get-sensor-statistic", async (IMediator mediator) =>
        {
            var resposne = await mediator.Send(new GetSensorStatisticQuery());

            return Results.Ok(resposne);
        }).RequireAuthorization();
    }
}
