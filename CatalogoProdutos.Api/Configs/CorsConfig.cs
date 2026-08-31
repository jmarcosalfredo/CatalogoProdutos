using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace CatalogoProdutos.Api.Configs
{
    public static class CorsConfig
    {
        public static readonly string OrigensComAcessoPermitido = "_origensComAcessoPermitido";

        public static IServiceCollection AddCorsConfig(this IServiceCollection services)
        {
            services.AddCors(options =>
            options.AddPolicy(name: OrigensComAcessoPermitido, policy =>
            {
                policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            })
            );

            return services;
        }
    }
}
