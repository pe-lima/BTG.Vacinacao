using BTG.Vacinacao.Infra.Configurations;
using BTG.Vacinacao.Application.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTG.Vacinacao.CrossCutting.Configurations;

namespace BTG.Vacinacao.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplication();
            services.AddInfra(configuration);
            services.AddCrossCutting();

            return services;
        }
    }
}
