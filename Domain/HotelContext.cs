using Domain.Entities.Skipass;
using Domain.Entities.Tariff;
using Domain.Entities.Visitor;
using Domain.Entities.VisitorsAction;
using Domain.EntitiesConfiguration;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;


namespace Domain;

public sealed class HotelContext : DbContext
{
    public HotelContext(DbContextOptions opts) : base(opts)
    {
    }

    public DbSet<VisitorRecord> Visitors => Set<VisitorRecord>();

    public DbSet<TariffRecord> Tariffs => Set<TariffRecord>();
    public DbSet<SkipassRecord> Skipasses => Set<SkipassRecord>();
    public DbSet<VisitorActionsRecord> VisitorActions => Set<VisitorActionsRecord>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.ApplyConfiguration(new SkipassConfiguration());
        modelBuilder.ApplyConfiguration(new TariffConfiguration());
        modelBuilder.ApplyConfiguration(new VisitorConfiguration());
        modelBuilder.ApplyConfiguration(new VisitorActionsConfiguration());
        
        modelBuilder.HasPostgresEnum<Place>();
    }
}