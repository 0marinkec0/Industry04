using Blazored.LocalStorage;
using System.Net.Http.Headers;

namespace Web.HttpHandlers
{
    public class SetAuthHeaderHandler : DelegatingHandler
    {
        private readonly ILocalStorageService _localStorageService;

        public SetAuthHeaderHandler(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var accessToken = await _localStorageService.GetItemAsync<string>("accessToken");

            if (!string.IsNullOrEmpty(accessToken))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            }
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
