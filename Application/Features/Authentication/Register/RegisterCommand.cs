using MediatR;
using Shared.ResultService;

namespace Application.Features.Authentication.Register
{
    public record RegisterCommand(string Email, string Password, string ConfirmPassword) : IRequest<Result>;
}
