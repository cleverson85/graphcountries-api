using Data;
using Data.Services;
using Domain.Interfaces;
using Domain.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IoC
{
    public static class InjectionServices
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddTransient<ICountryService, CountryService>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
