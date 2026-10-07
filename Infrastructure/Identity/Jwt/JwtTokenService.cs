using Application.Dto;
using Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Identity.Jwt;

public class JwtTokenService : IJwtTokenService
{
    private readonly JwtSettings _jwtSettings;
    private readonly UserManager<User> _userManager;
    public JwtTokenService(IOptions<JwtSettings> jwtSettings, UserManager<User> userManager)
    {
        _jwtSettings = jwtSettings.Value;
        _userManager = userManager;
    }

    public async Task<TokensDTO> GenerateUserTokenService(List<Claim> claims, User user)
    {
        var accessToken = GenerateAccessToken(claims);
        var refreshToken = GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.ExpiresOnUtc = DateTime.Now.AddDays(7);

        await _userManager.UpdateAsync(user);

        return new TokensDTO()
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }

    private string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
    }
    private string GenerateAccessToken(List<Claim> claims)
    {
        var expiresAt = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes);

        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key!));

        var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

        var descriptior = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = expiresAt,
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience,
            SigningCredentials = credentials
        };

        return new JsonWebTokenHandler().CreateToken(descriptior);
    }
}
