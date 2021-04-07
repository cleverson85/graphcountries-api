using Data.Repositories;
using Domain.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace IoC
{
    public static class InjectionRepositories
    {
        public static IServiceCollection ConfigureRepositories(this IServiceCollection services)
        {
            services.AddTransient<ICountryRepository, CountryRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

            return services;
        }
    }
}
