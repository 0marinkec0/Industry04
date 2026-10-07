using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using System.Net.Http;

namespace BlazorWeb.Services
{
    public class LogoutService
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly NavigationManager _navigationManager;
        private readonly RefreshTokenService _refreshTokenService;

        public LogoutService(ILocalStorageService localStorageService, NavigationManager navigationManager, RefreshTokenService refreshTokenService)
        {
            _localStorageService = localStorageService;
            _navigationManager = navigationManager;
            _refreshTokenService = refreshTokenService;
        }
        public async Task Logout()
        {
            try
            {
                await _refreshTokenService.RevokeRefreshToken();
            }
            catch
            { 
            }
            finally
            {
                await _localStorageService.ClearAsync();
                _navigationManager.NavigateTo("login", true);
            }

        }
    }
}
