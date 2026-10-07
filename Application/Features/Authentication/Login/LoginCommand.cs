using Application.Dto;
using MediatR;
using Shared.Respons;
using Shared.ResultService;

namespace Application.Features.Authentication.Login;

public record LoginCommand(string Email, string Password) : IRequest<Result<TokensDTO>>;
