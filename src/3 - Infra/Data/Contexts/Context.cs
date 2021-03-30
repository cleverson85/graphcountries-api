using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Data.Contexts
{
    public class Context : DbContext
    {
        public DbSet<CountryData> CountryData { get; set; }

        public Context(DbContextOptions<Context> options) : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
