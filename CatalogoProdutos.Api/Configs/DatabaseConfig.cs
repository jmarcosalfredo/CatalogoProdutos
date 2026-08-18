using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogoProdutos.Api.Context;
using Microsoft.EntityFrameworkCore;

namespace CatalogoProdutos.Api.Configs
{
    public static class DatabaseConfig
    {
        public static IServiceCollection AddDatabaseConfigurarion(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["ConnectionStrings:CatalogoConnection"];
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string 'ConnectionStrings:CatalogoConnection' not found in configuration.");
            }

            services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

            return services;
        }
    }
}
