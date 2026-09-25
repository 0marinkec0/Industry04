using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Web.Services
{
    public class JwtAuthenticationStateProvide : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;

        private static readonly AuthenticationState Anonymous = new(new ClaimsPrincipal(new ClaimsIdentity()));

        public JwtAuthenticationStateProvide(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var accessToken = await _localStorage.GetItemAsync<string>("accessToken");

            if (string.IsNullOrWhiteSpace(accessToken))
                return Anonymous;

            return new AuthenticationState(new ClaimsPrincipal
                (new ClaimsIdentity(ParseClaimsFromJwt(accessToken), "jwtAuth")));
        }

        public async Task InitializeAsync()
        {
            var token = await _localStorage.GetItemAsync<string>("accessToken");

            if (string.IsNullOrWhiteSpace(token))
            {
                NotifyAuthenticationStateChanged(Task.FromResult(Anonymous));

                return;
            }

            var claims = ParseClaimsFromJwt(token);

            var identity = new ClaimsIdentity(claims, "jwt");

            var user = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var handler = new JwtSecurityTokenHandler();

            var token = handler.ReadJwtToken(jwt);

            return token.Claims;
        }
    }
}
