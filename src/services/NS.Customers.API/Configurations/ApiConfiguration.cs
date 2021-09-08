using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NS.Customers.API.Data;
using NS.WebApi.Core.Identities;

namespace NS.Customers.API.Configurations
{
    public static class ApiConfiguration
    {
        public static IServiceCollection AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CustomersContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddControllers();

            services.AddCors(options => { options.AddPolicy("All", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()); });

            return services;
        }

        public static IApplicationBuilder UseApiConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("All");

            app.UseAuthConfiguration();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            return app;
        }
    }
}
