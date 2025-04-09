using BTG.Vacinacao.Core.Interfaces.Repository;
using BTG.Vacinacao.Infra.Context;
using BTG.Vacinacao.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BTG.Vacinacao.Infra.Configurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfra(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection")).EnableSensitiveDataLogging());
            
            // ( Repositories )
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IVaccineRepository, VaccineRepository>();

            return services;
        }

    }
}
