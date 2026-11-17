using Application.Features.Sensor.Queries.GetAllTemperature;
using Carter;
using MediatR;

namespace API.MinimalAPI.SensorAPI;

public class GetAllTemperatureEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("sensor/get-all-temperature-measurements", async (IMediator mediator) =>
        {
            //   var temeprature = await service.GetAllTemperatureMeasurements();

            var temeprature = await mediator.Send(new GetAllTemperatureQuery(), default);
            
            return Results.Ok(temeprature);

        }).RequireAuthorization();
    }
}
