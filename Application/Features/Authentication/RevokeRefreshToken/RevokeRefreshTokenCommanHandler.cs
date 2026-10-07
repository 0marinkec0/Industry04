using Application.Interfaces;
using MediatR;
using Shared.ResultService;

namespace Application.Features.Authentication.RevokeRefreshToken;

public class RevokeRefreshTokenCommanHandler : IRequestHandler<RevokeRefreshTokenCommand, Result>
{
    private readonly IIdentityService _identityService;

    public RevokeRefreshTokenCommanHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Result> Handle(RevokeRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        return await _identityService.RevokeRefreshToken(request.email);
    }
}
