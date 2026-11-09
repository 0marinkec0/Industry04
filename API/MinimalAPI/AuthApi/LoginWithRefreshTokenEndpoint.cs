using Application.Features.Authentication.LoginWithRefreshToken;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace API.MinimalAPI.AuthApi;

public class LoginWithRefreshTokenEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("auth/login-with-refresh-token", async (IMediator mediator, HttpRequest httpRequest, HttpResponse httpResponse) =>
        {
           var refreshToken = httpRequest.Cookies["refresh_token"];

           var command = new LoginWithRefreshTokenCommand(refreshToken);

            var result = await mediator.Send(command);

            httpResponse.Cookies.Append("refresh_token", result.Value.RefreshToken!, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Path = "/",
                MaxAge = TimeSpan.FromDays(7)
            });

            if (result.IsSuccess)
                return Results.Ok(result.Value);

            return Results.BadRequest();
        });
    }
}
