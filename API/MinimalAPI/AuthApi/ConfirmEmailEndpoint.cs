using Application.Features.Authentication.ConfirmEmail;
using Carter;
using MediatR;

namespace API.MinimalAPI.AuthApi;
public class ConfirmEmailEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("auth/confirm-email", async (ConfirmEmailCommand command, IMediator mediator) =>
        {
            var result = await mediator.Send(command);

            if (result.IsSuccess)
                return Results.Ok();

            return Results.BadRequest();
        });
    }
}
