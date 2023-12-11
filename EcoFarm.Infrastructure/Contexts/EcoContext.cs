using EcoFarm.Domain.Entities.Administration;
using Microsoft.EntityFrameworkCore;

namespace EcoFarm.Infrastructure.Contexts;

public class EcoContext : DbContext
{
    public EcoContext() { }
    public EcoContext(DbContextOptions<EcoContext> options) : base(options) { }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    optionsBuilder.UseNpgsql();
    //}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
        {
            property.SetColumnType("decimal(18,2)");
        }

        //foreach (var property in modelBuilder.Model.GetEntityTypes()
        //        .SelectMany(t => t.GetProperties())
        //        .Where(p => p.ClrType == typeof(string)))
        //{
        //    property.SetColumnType("varchar(256)");
        //}


        //modelBuilder.Entity<Account>
    }

    public DbSet<Account> Users { get; set; }
    //public DbSet<Role> Roles { get; set; }


}