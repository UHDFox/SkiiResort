using Domain.Entities.Location;
using Domain.Entities.Skipass;
using Domain.Entities.Tariff;
using Domain.Entities.Tariffication;
using Domain.Entities.Visitor;
using Domain.Entities.VisitorsAction;
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

    public DbSet<TarifficationRecord> Tariffications => Set<TarifficationRecord>();

    public DbSet<LocationRecord> Locations => Set<LocationRecord>();

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}