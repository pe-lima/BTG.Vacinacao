using BTG.Vacinacao.Core.Interfaces.Services;
using BTG.Vacinacao.CrossCutting.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTG.Vacinacao.CrossCutting.Configurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCrossCutting(this IServiceCollection services)
        {
            services.AddScoped<IJwtService, JwtService>();
            return services;
        }
    }
}
