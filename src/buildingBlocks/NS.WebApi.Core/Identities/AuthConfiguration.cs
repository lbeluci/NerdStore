using Microsoft.AspNetCore.Builder;

namespace NS.WebApi.Core.Identities
{
    public static class AuthConfiguration
    {
        public static IApplicationBuilder UseAuthConfiguration(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();

            return app;
        }
    }
}
