using Application.Dto;
using Infrastructure.Identity;
using System.Security.Claims;

namespace Application.Interfaces;

public interface IJwtTokenService
{
    public Task<TokensDTO> GenerateUserTokenService(List<Claim> claims, User user);
}
