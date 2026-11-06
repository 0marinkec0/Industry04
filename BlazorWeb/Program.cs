using Blazored.LocalStorage;
using BlazorWeb;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Web.HttpHandlers;
using Web.Services;
using BlazorWeb.Services;
using BlazorWeb.HttpHandlers;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddAuthorizationCore();

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped<HubService>();

builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthenticationStateProvide>();

builder.Services.AddScoped<LogoutService>();

builder.Services.AddTransient<SetAuthHeaderHandler>();
builder.Services.AddTransient<ExceptionDelegatingHandler>();
builder.Services.AddTransient<JwtAuthorizationHandler>();
builder.Services.AddTransient<RefreshTokenHandler>();
builder.Services.AddTransient<IncludeCookieHandler>();

builder.Services.AddHttpClient<ApiService>(httpClient =>
{
    httpClient.BaseAddress = new Uri("https://localhost:7244");
})
    //.AddHttpMessageHandler<JwtAuthorizationHandler>()
    .AddHttpMessageHandler<IncludeCookieHandler>()
    .AddHttpMessageHandler<SetAuthHeaderHandler>()
    .AddHttpMessageHandler<RefreshTokenHandler>()
    .AddHttpMessageHandler<ExceptionDelegatingHandler>();

builder.Services.AddHttpClient<RefreshTokenService>(httpClient => 
{
    httpClient.BaseAddress = new Uri("https://localhost:7244");
}).AddHttpMessageHandler<IncludeCookieHandler>();

builder.Logging.SetMinimumLevel(LogLevel.Trace);

await builder.Build().RunAsync();
