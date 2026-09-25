using Application.Interfaces;
using MediatR;
using Shared.ResultService;

namespace Application.Features.Authentication.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result>
    {
        private readonly IIdentityService _identityService;
        private readonly IEmailService _emailService;
        public RegisterCommandHandler(IIdentityService identityService, IEmailService emailService)
        {
            _identityService = identityService;
            _emailService = emailService;
        }

        public async Task<Result> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var registerResponse = await _identityService.Register(request.Email, request.Password);

            if (registerResponse.IsSuccess)
            {
                var token = await _identityService.GenerateConfirmationEmailTokenAsync(request.Email);

                if(token.IsSuccess)
                await _emailService.SendConfirmationEmail(request.Email, token.Value);                
            }

            return registerResponse;
        }
    }
}
