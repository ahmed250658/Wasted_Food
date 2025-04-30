using Microsoft.Extensions.DependencyInjection;
using Wasted_Food.Infrastructure.Repository.Abstracts;
using Wasted_Food.Infrastructure.Repository.Implements;

namespace Wasted_Food.Infrastructure
{
    public static class ModuleInfrastructureDependencies
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
        {
            services.AddTransient<IRefreshTokenReposatory, RefreshTokenReposatory>();
            services.AddTransient<IDonationRepository, DonationRepository>();
            services.AddTransient<IFoodRequestRepository, FoodRequestRepository>();
            return services;
        }

    }
}
