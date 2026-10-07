using Application.Dto;
using MediatR;
using Shared.ResultService;

namespace Application.Features.Authentication.LoginWithRefreshToken;

public record LoginWithRefreshTokenCommand(string refreshToken) : IRequest<Result<TokensDTO>>;
