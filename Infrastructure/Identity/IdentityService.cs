using Application.Dto;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Shared.ResultService;

namespace Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly UserManager<User> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<IdentityService> _logger;
    private readonly SignInManager<User> _signInManager;
    private readonly IJwtTokenService _jwtTokenService;
    public IdentityService(UserManager<User> userManager, IHttpContextAccessor httpContextAccessor, ILogger<IdentityService> logger, SignInManager<User> signInManager, IJwtTokenService jwtTokenService)
    {
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
        _signInManager = signInManager;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<Result<TokensDTO>> Login(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user is null)
        {
            return Result<TokensDTO>.Failure(new Error(code: "Validaiton.EmailNotFound", message: $"User with email {email} is not found"));
        }

        var checkPassword = await _userManager.CheckPasswordAsync(user, password);

        if (checkPassword is false)
        {
            return Result<TokensDTO>.Failure(new Error(code: "DomainError.WrongPassword", message: $"Wrong password"));
        }

        var checkIfEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);

        if (checkIfEmailConfirmed is false)
        {
            return Result<TokensDTO>.Failure(new Error(code: "DomainError.EmailNotConfirmed", message: $"Email {email} is not confirmed"));
        }

        var claims = User.GetClaims(user);
      
        var token = await _jwtTokenService.GenerateUserTokenService(claims, user);

        return Result<TokensDTO>.Success(token);
    }

    public async Task<Result<TokensDTO>> LoginWithRefreshToken(string refreshToken)
    {
        if (string.IsNullOrEmpty(refreshToken))
        {
            return Result<TokensDTO>.Failure(new Error("DomainError.RefreshToken", message: "Refresh token is null or empty"));
        }

        var user = _userManager.Users.FirstOrDefault(x => x.RefreshToken == refreshToken);

        if(user is null || user.ExpiresOnUtc < DateTime.UtcNow)
        {
            return Result<TokensDTO>.Failure(new Error("DomainError.RefreshToken", message: "Refresh token has expired"));
        }

        var claims = User.GetClaims(user);

        var token = await _jwtTokenService.GenerateUserTokenService(claims, user);;

        return Result<TokensDTO>.Success(token);
    }

    public async Task<Result> Register(string email, string password)
    {
        var user = User.Create(email,email);

        var registerUser = await _userManager.CreateAsync(user, password);

        var errors = registerUser.Errors.Select(a => new Error(code: "Validaiton.UserManager", message: a.Description)).ToArray();

        return registerUser.Succeeded ? Result.Success() : ValidationErrors.WithErrors(errors);
    }

    public async Task<Result<string>> GenerateConfirmationEmailTokenAsync(string email)
    {
        var user = await CheckUserByEmail(email);

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user.Value);

        //sencd email service
        _logger.LogInformation(token);

        return Result<string>.Success(token);
    }

    public async Task<Result> RevokeRefreshToken(string email)
    {
        var user = CheckUserByEmail(email);

        user.Result.Value.RefreshToken = null;
        user.Result.Value.ExpiresOnUtc = null;

        await _userManager.UpdateAsync(user.Result.Value);

        return Result.Success();
    }

    public async Task<Result> ConfirmEmailAsync(string email, string token)
    {
        var user = await CheckUserByEmail(email);

        await _userManager.ConfirmEmailAsync(user.Value, token);

        return Result.Success();
    }

    private async Task<Result<User>> CheckUserByEmail(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user is null)
        {
            return Result<User>.Failure(new Error(code: "DomainError.ConfrimEmailError", message: $"User with {email} is not found"));
        }

        return Result<User>.Success(user);
    }        
}
