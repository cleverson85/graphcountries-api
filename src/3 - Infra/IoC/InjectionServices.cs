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
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAuthJwtService, AuthJwtService>();

            return services;
        }
    }
}
