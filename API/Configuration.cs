using API.Services;
using Application;
using Application.Interfaces;
using Carter;
using Infrastructure;
using Infrastructure.Database.Seeder;
using Infrastructure.SignalR.Hubs;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace API
{
    public static class Configuration
    {
        public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services
                .AddApplicationSerivces()
                .AddInfrastructureServices(builder.Configuration)
                .AddApiServices(builder.Configuration);


            return builder;
        }
        public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options => options.CustomSchemaIds(type => type.FullName));

            services.AddScoped<IAlarmService, AlarmService>();

            services.AddAutoMapper(config => { }, Assembly.GetExecutingAssembly());

            services.AddCarter();

             services.AddCors(options =>
            {
                options.AddPolicy("BlazorPolicy", policy =>
                {
                    policy.WithOrigins("https://localhost:7051")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                });
            });

            return services;
        }

        public static WebApplication ConfigureApplication(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("BlazorPolicy");

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapCarter();

            app.MapHub<SensorReadingNotificationHub>("/sensor-readings-hub");

            using (var scope = app.Services.CreateScope())
            {
                var seeder = scope.ServiceProvider
                    .GetRequiredService<ActuatorSeeder>();

                seeder.SeedAsync().GetAwaiter().GetResult();
            }

            return app;
        }
    }
}

