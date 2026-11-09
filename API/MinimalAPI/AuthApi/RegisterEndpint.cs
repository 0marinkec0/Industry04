using API.Extensions;
using Application.Features.Authentication.Register;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.MinimalAPI.AuthApi;
public class RegisterEndpint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("auth/register", async ([FromBody] RegisterCommand command, IMediator mediator) =>
        {
            Console.WriteLine("ENTERED AUTH REGISTER ENDPOINT");
            var result = await mediator.Send(command);

            if (result.IsSuccess)
                return Results.Ok();

            return FailureHandler.HandleFailure(result);
        });
    }
}
