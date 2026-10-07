using Application.Features.Authentication.LoginWithRefreshToken;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;

namespace API.MinimalAPI.AuthApi
{
    public class LoginWithRefreshTokenEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("auth/login-with-refresh-token", async (IMediator mediator, HttpRequest httpRequest) =>
            {
               var refreshToken = httpRequest.Cookies["refresh_token"];

               var command = new LoginWithRefreshTokenCommand(refreshToken);

                var result = await mediator.Send(command);

                if (result.IsSuccess)
                    return Results.Ok(result.Value);

                return Results.BadRequest();
            });
        }
    }
}
