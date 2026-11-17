using Microsoft.AspNetCore.WebUtilities;

namespace BlazorWeb.Services;

public class ApiService
{
    private readonly HttpClient _httpClient;

    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<TResponse> Get<TResponse>(string relativeUri)
    {
        var httpResponse = await _httpClient.GetAsync(relativeUri, HttpCompletionOption.ResponseContentRead);
        return (await httpResponse.Content.ReadFromJsonAsync<TResponse>())!;
    }
    public async Task Post<TRequest>(string relativeUrl, TRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync(relativeUrl, request);

        var content = await response.Content.ReadAsStringAsync();
    }
    public async Task<TResponse> Post<TRequest, TResponse>(string relativeUrl, TRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync(relativeUrl, request);

        return (await response.Content.ReadFromJsonAsync<TResponse>())!;
    }
    public async Task PostQuery(string relativeUrl, Dictionary<string, string> queryParams)
    {
        var url = QueryHelpers.AddQueryString(relativeUrl, queryParams!);

        var resposne = await _httpClient.PostAsync(url, null);
    }
    public async Task Delete(string relativeUrl)
    { 
        var response = await _httpClient.DeleteAsync(relativeUrl);

        var content = await response.Content.ReadAsStringAsync();
    }
    public async Task Put<TRequest>(string relativeUrl, TRequest request)
    {
        var response = await _httpClient.PutAsJsonAsync<TRequest>(relativeUrl, request);

        var content = await response.Content.ReadAsStringAsync();
    }
}
