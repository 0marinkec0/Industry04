using API.Extensions;
using Application.Features.Authentication.Login;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.MinimalAPI.AuthApi;

public class LoginEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("auth/login", async ([FromBody] LoginCommand command, IMediator mediator) =>
        {
            var result = await mediator.Send(command);
            Console.WriteLine("ENTERED AUTH LOGIN ENDPOINT");

            if (result.IsSuccess)
                return Results.Ok(result.Value);

            return FailureHandler.HandleFailure(result);
        });
    }
}
