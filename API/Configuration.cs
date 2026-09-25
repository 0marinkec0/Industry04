using API.Services;
using Application;
using Application.Interfaces;
using Carter;
using Infrastructure;
using Infrastructure.SignalR.Hubs;

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
            services.AddSwaggerGen();          
                       
            services.AddScoped<IAlarmService, AlarmService>();

            services.AddCarter();

            return services;
        }

        public static WebApplication ConfigureApplication(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHttpsRedirection();

            app.MapCarter();

            app.MapHub<SensorReadingNotificationHub>("/sensor-readings-hub");

            return app;
        }
    }
}

