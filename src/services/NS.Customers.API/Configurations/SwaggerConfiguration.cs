using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

namespace NS.Customers.API.Configurations
{
    public static class SwaggerConfiguration
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(setup =>
            {
                setup.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "NerdStore Customers API",
                    Description = "NerdStore Customers API",
                    Contact = new OpenApiContact
                    {
                        Name = "Luiz Henrique Beluci Terra",
                        Email = "lbeluci@gmail.com"
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT",
                        Url = new Uri("https://opensource.org/licenses/MIT")
                    }
                });

                setup.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Insert the JWT token this way: Bearer {your token}",
                    Name = "Authorization",
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(setup =>
            {
                setup.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });

            return app;
        }
    }
}
