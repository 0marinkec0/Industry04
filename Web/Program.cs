using Blazored.LocalStorage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Web.Components;
using Web.HttpHandlers;
using Web.Services;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddAuthentication();

 builder.Services.AddAuthorizationCore();

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddCascadingAuthenticationState();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped<HubService>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthenticationStateProvide>();

builder.Services.AddTransient<ExceptionDelegatingHandler>();
builder.Services.AddTransient<JwtAuthorizationHandler>();

builder.Services.AddHttpClient<ApiService>(httpClient =>
{
    httpClient.BaseAddress = new Uri("https://localhost:7244");
})
    // .AddHttpMessageHandler<JwtAuthorizationHandler>()
    .AddHttpMessageHandler<ExceptionDelegatingHandler>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseAuthentication();
//app.UseAuthorization();

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AllowAnonymous();

app.Run();
