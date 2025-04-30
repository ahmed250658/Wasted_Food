using Microsoft.Extensions.DependencyInjection;
using Wasted_Food.Service.Abstracts;
using Wasted_Food.Service.AuthService.Abstracts;
using Wasted_Food.Service.AuthService.implementions;

using Wasted_Food.Service.Implementions;

namespace Wasted_Food.Service
{
    public static class ModuleServiceDependencies
    {
        public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
        {
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IDonationService, DonationService>();
            services.AddTransient<IFoodRequestService, FoodRequestService>();
            services.AddTransient<IFileService, FileService>();
            services.AddTransient<ICurrentUserService, CurrentUserService>();
            return services;
        }
    }
}
