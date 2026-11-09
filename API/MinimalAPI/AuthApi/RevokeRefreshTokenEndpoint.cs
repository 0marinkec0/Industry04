using Application.Features.Authentication.RevokeRefreshToken;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.MinimalAPI.AuthApi;
public class RevokeRefreshTokenEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("auth/revoke-refresh-token", async ([FromBody]RevokeRefreshTokenCommand command ,IMediator mediator, HttpResponse response) =>
        {
            var result = mediator.Send(command);

            if (result.Result.IsSuccess)
            {
                response.Cookies.Delete("refresh_token", new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Path = "/"
                });

                return Results.Ok();
            }
            return Results.BadRequest();
        });
    }
}
