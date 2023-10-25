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
}