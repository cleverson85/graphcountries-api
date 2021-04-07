using Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IoC
{
    public static class InjectionContext
    {
        public static IServiceCollection ConfigureContext(this IServiceCollection services, string connection)
        {
            return services.AddDbContext<Context>(options => options.UseNpgsql(connection));
        }
    }
}