using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.RateLimiting;
using System.Threading.Tasks;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CatalogoProdutos.Api.Configs
{
    public static class RateLimiterConfig
    {
        public static IServiceCollection AddRateLimiterConfig(this IServiceCollection services)
        {
            services.AddRateLimiter(rateLimiterOptions =>
            {
                rateLimiterOptions.AddFixedWindowLimiter("fixedwindow", options =>
                {
                    options.PermitLimit = 3;
                    options.Window = TimeSpan.FromSeconds(10);
                    options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    options.QueueLimit = 1;
                });

                rateLimiterOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
            });

            return services;
        }
    }
}
