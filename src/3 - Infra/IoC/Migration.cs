using Data.Contexts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IoC
{
    public static class Migration
    {
        public static IHost MigrateDbContext<TContext>(this IHost host) where TContext : DbContext
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetService<TContext>();
                context.Database.Migrate();
#if (DEBUG)
                InserirUsuarioAdmin(context as Context);
#endif
            }

            return host;
        }
        private static void InserirUsuarioAdmin(Context context)
        {
            var user = context.Find<User>(1);

            if (user == null)
            {
                context.Add(new User
                {
                    Email = "usuario@admin.com.br",
                    Senha = "123456"

                });

                context.SaveChanges();
            }
        }
    }
}
