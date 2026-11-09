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
            var token = await _localStorageService.GetItemAsync<string>("accessToken");

            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }
        catch (Exception ex)
        {
            Console.Write(ex.ToString());
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
