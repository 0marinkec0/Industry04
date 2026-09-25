using Application.Features.Queries.GetAllValuesSensorType;
using Carter;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.MinimalAPI.SensorAPI
{
    public class GetAllValuesSensorTypeEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/sensors/get-all-values-by-type", async ([FromQuery] SensorType type, IMediator mediator) =>
            {
               var response =  await mediator.Send(new GetAllValuesSensorTypeQuery(type));

               return Results.Ok(response);
            });
        }
    }
}
