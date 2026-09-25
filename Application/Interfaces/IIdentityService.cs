using Application.Dto;
using Shared.Respons;
using Shared.ResultService;

namespace Application.Interfaces;

public interface IIdentityService
{
    Task<Result> Register(string email,  string password);
    Task<Result<TokensDTO>> Login(string email, string password);
    Task<Result<string>> GenerateConfirmationEmailTokenAsync(string email);
    Task<Result> ConfirmEmailAsync(string email, string token);
}
