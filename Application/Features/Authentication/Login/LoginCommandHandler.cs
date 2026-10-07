using Application.Dto;
using Application.Interfaces;
using MediatR;
using Shared.Respons;
using Shared.ResultService;

namespace Application.Features.Authentication.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<TokensDTO>>
{
    private readonly IIdentityService _identityService;

    public LoginCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Result<TokensDTO>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
         var result = await _identityService.Login(request.Email, request.Password);

         return result;
    }
}
