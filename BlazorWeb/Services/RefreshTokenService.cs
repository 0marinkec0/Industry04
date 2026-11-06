using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Shared.Respons.Auth;
using System.Net.Http.Json;
using System.Security.Claims;
using Web.Services;

namespace BlazorWeb.Services;
public class RefreshTokenService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorageService;
    private readonly AuthenticationStateProvider _authenticationStateProvider;

    public RefreshTokenService(HttpClient httpClient, ILocalStorageService localStorageService, AuthenticationStateProvider authenticationStateProvider)
    {
        _httpClient = httpClient;
        _localStorageService = localStorageService;
        _authenticationStateProvider = authenticationStateProvider;
    }

    public async Task<bool> GetRefreshTokenAsync()
    {
        var request = new HttpRequestMessage(HttpMethod.Post, "auth/login-with-refresh-token");

        var response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
            return false;

        var tokenResponse = await response.Content.ReadFromJsonAsync<TokenResponse>();

        await _localStorageService.SetItemAsync("accessToken", tokenResponse!.AccessToken);
        ((JwtAuthenticationStateProvide)_authenticationStateProvider).NotifyUserAuthentication(tokenResponse.AccessToken!);

        return true;
    }

    public async Task RevokeRefreshToken()
    {
        var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();

        var user = authState.User;

        string email = user.FindFirst(ClaimTypes.Email)!.Value;

        var response = await _httpClient.PostAsJsonAsync("auth/revoke-refresh-token", new { email = email});
    }
}
