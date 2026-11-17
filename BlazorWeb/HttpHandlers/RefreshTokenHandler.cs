using Blazored.LocalStorage;
using BlazorWeb.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;

namespace BlazorWeb.HttpHandlers;

public class RefreshTokenHandler : DelegatingHandler
{
    private readonly AuthenticationStateProvider _autheticationStateProvider;
    private readonly RefreshTokenService _refreshTokenService;
    private readonly ILocalStorageService _localStorageService;
    private readonly NavigationManager _navigationManager;
    public RefreshTokenHandler(AuthenticationStateProvider autheticationStateProvider, RefreshTokenService refreshTokenService, ILocalStorageService localStorageService, NavigationManager navigationManager)
    {
        _autheticationStateProvider = autheticationStateProvider;
        _refreshTokenService = refreshTokenService;
        _localStorageService = localStorageService;
        _navigationManager = navigationManager;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (await ShouldRefreshToken())
        {
            if (await _refreshTokenService.GetRefreshTokenAsync())
            {
                var accessToken = await _localStorageService.GetItemAsync<string>("accessToken");
                 request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            }
            else
            {
                await _localStorageService.RemoveItemAsync("accessToken");
                _navigationManager.NavigateTo("login", true);
            }
        }
        return await base.SendAsync(request, cancellationToken);
    }

    private async Task<bool> ShouldRefreshToken()
    {
        var authState = await _autheticationStateProvider.GetAuthenticationStateAsync();

        var user = authState.User;

        if (!user.Identity!.IsAuthenticated)
            return false;

        var expClaim = user.FindFirst("exp");

        if (!long.TryParse(expClaim?.Value, out var exp))
            return false;

        var expirationTime = DateTimeOffset.FromUnixTimeSeconds(exp);

        var remainingMinutes = (expirationTime - DateTimeOffset.UtcNow).TotalMinutes;

        if (remainingMinutes <= 2)
            return true;

        return false;
    }
}
