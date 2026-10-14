using Application.Features.Queries.GetLatestSensorReadings;
using Carter;
using MediatR;

namespace API.MinimalAPI.SensorApi
{
    public class GetLatestSensorReadingEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("sensor/get-latest-sensor-readings", async (IMediator mediator) =>
            {

                var result = await mediator.Send(new GetLatestSensorReadingQuery());

                return Results.Ok(result);
            });
        }
    }
}
