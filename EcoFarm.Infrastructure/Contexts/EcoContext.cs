using EcoFarm.Domain.Entities.Administration;
using Microsoft.EntityFrameworkCore;

namespace EcoFarm.Infrastructure.Contexts;

public class EcoContext : DbContext
{
    public EcoContext() { }
    public EcoContext(DbContextOptions<EcoContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Account> Users { get; set; }
    //public DbSet<Role> Roles { get; set; }


}