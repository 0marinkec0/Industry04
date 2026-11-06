using Blazored.LocalStorage;
using Shared.Respons.Auth;
using System.Net.Http.Headers;

namespace Web.HttpHandlers;

public class JwtAuthorizationHandler : DelegatingHandler
{
    private readonly ILocalStorageService _localStorageService;

    public JwtAuthorizationHandler(ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        try
        {
            var token = await _localStorageService.GetItemAsync<TokenResponse>("accessToken");

            if (!string.IsNullOrEmpty(token.AccessToken))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
            }
        }
        catch (Exception ex)
        {
            Console.Write(ex.ToString());
        }

        return await base.SendAsync(request, cancellationToken);
    }

}
