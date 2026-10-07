using MediatR;
using Shared.ResultService;

namespace Application.Features.Authentication.RevokeRefreshToken;

public record RevokeRefreshTokenCommand(string email) : IRequest<Result>;
