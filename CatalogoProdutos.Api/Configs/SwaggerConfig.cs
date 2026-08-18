using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoProdutos.Api.Configs
{
    public static class SwaggerConfig
    {
        private static readonly string appName = "Catalogo Genérico de Produtos";

        public static IApplicationBuilder UseSwaggerConfig(this IApplicationBuilder app)
        {
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/openapi/v1.json", appName);
                options.RoutePrefix = "swagger";
                options.DocumentTitle = appName;
            });

            return app;
        }
    }
}
