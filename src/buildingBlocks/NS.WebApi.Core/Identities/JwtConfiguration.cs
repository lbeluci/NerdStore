using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace NS.WebApi.Core.Identities
{
    public static class JwtConfiguration
    {
        public static IServiceCollection AddJwtConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(GetAuthenticationOptions()).AddJwtBearer(GetBearerOptions(services, configuration));

            return services;
        }

        private static Action<AuthenticationOptions> GetAuthenticationOptions()
        {
            return options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            };
        }

        private static Action<JwtBearerOptions> GetBearerOptions(IServiceCollection services, IConfiguration configuration)
        {
            var appSettings = GetAppSettings(services, configuration);
            var key = GetKey(appSettings);

            return bearerOptions =>
            {
                bearerOptions.RequireHttpsMetadata = true;
                bearerOptions.SaveToken = true;
                bearerOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = appSettings.Emitter,
                    ValidateAudience = true,
                    ValidAudience = appSettings.ValidIn
                };
            };
        }

        private static AppSettings GetAppSettings(IServiceCollection services, IConfiguration configuration)
        {
            var appSettingsSection = configuration.GetSection("AppSettings");

            services.Configure<AppSettings>(appSettingsSection);

            return appSettingsSection.Get<AppSettings>();
        }

        private static byte[] GetKey(AppSettings appSettings)
        {
            return Encoding.ASCII.GetBytes(appSettings.Secret);
        }
    }
}