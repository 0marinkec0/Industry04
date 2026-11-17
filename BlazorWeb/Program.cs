using BlazorWeb;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args)
    .AddRootComponents()
    .AddClientServices();

builder.Logging.SetMinimumLevel(LogLevel.Trace);

await builder.Build().RunAsync();
