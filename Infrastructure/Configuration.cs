using Application.Factory;
using Application.Interfaces;
using Infrastructure.Database;
using Infrastructure.Identity;
using Infrastructure.Identity.Jwt;
using Infrastructure.Messaging;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MQTTnet;
using System.IdentityModel.Tokens.Jwt;
using System.Text;


namespace Infrastructure
{
    public static class Configuration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));


            var jwtSettings = configuration.GetSection("JwtSettings")
                .Get<JwtSettings>() ?? throw new InvalidOperationException("Jwt  settings missing");

            services.AddIdentity<User, IdentityRole<int>>(options =>
            {
                options.Password.RequiredLength = 3;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = false;
                options.Lockout.AllowedForNewUsers = false;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));


            services.AddAuthentication(options => 
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options => {
                options.MapInboundClaims = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {

                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
                    ClockSkew = TimeSpan.Zero,
                    NameClaimType = JwtRegisteredClaimNames.Name,
                    RoleClaimType = "role"
                };
            });

            services.AddSignalR();

            services.AddAuthorization();

            services.AddHttpContextAccessor();

            services.AddScoped<IAppDbContext, AppDbContext>();

            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IJwtTokenService, JwtTokenService>();

            services.AddTransient<IIdentityService, IdentityService>(); 
            services.AddTransient<ISensorFactory, TemperatureSensor>();
            services.AddTransient<ISensorFactory, HumiditySensor>();
            services.AddTransient<SensorProcessor>();

            services.AddScoped<IMqttMessageHandler, MqttMessageHandler>();
            services.AddSingleton<IMqttService, MqttService>();
            services.AddHostedService<MqttBackgroundService>();

            services.Configure<MqttOptions>(configuration.GetSection("Mqtt"));

            services.AddSingleton(sp =>
            {
                var factory = new MqttClientFactory();

                var client = factory.CreateMqttClient();

                return client;
            });

            return services;
        }
    }
}
