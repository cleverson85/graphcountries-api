using Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IoC
{
    public static class InjectionContext
    {
        public static IServiceCollection ConfigureContext(this IServiceCollection services, string conNection)
        {
            return services.AddDbContext<Context>(options => options.UseNpgsql(conNection));
        }
    }
}
