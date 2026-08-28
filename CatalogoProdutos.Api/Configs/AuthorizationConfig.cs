using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoProdutos.Api.Configs
{
    public static class AuthorizationConfig
    {
        public static IServiceCollection AddAuthorizationConfig(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
                options.AddPolicy("SuperAdminOnly", policy => policy.RequireRole("Admin").RequireClaim("id", "joaoAdmin"));
                options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));
                options.AddPolicy("ExclusiveOnly", policy => policy.RequireAssertion(context =>
                                                    context.User.HasClaim(claim => claim.Type == "id" && claim.Value == "joaoAdmin" || context.User.IsInRole("SuperAdmin"))));
            });

            return services;
        }
    }
}
