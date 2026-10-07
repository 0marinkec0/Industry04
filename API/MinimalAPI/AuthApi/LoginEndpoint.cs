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
        app.MapPost("auth/login", async ([FromBody] LoginCommand command, IMediator mediator, HttpResponse httpResponse) =>
        {
            var result = await mediator.Send(command);
            Console.WriteLine("ENTERED AUTH LOGIN ENDPOINT");

            if (!result.IsSuccess)
                return FailureHandler.HandleFailure(result);

            httpResponse.Cookies.Append("refresh_token", result.Value.RefreshToken!, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Path = "/",
                MaxAge = TimeSpan.FromDays(7)
            });
  
             return Results.Ok(result.Value);
        });
    }
}
