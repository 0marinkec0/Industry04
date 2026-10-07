using Application.Dto;
using Application.Interfaces;
using MediatR;
using Shared.ResultService;

namespace Application.Features.Authentication.LoginWithRefreshToken;

public class LoginWithRefreshTokenCommandHandler : IRequestHandler<LoginWithRefreshTokenCommand, Result<TokensDTO>>
{
    private readonly IIdentityService _identityService;

    public LoginWithRefreshTokenCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Result<TokensDTO>> Handle(LoginWithRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        return await _identityService.LoginWithRefreshToken(request.refreshToken);
    }
}
