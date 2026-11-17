
using Microsoft.AspNetCore.Components.WebAssembly.Http;

namespace BlazorWeb.HttpHandlers;

public class IncludeCookieHandler : DelegatingHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
        return base.SendAsync(request, cancellationToken);
    }
}
