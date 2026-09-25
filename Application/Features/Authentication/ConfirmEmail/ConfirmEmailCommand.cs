using MediatR;
using Shared.ResultService;

namespace Application.Features.Authentication.ConfirmEmail;

public record ConfirmEmailCommand(string email, string token) : IRequest<Result>;
