using Microsoft.EntityFrameworkCore;
using SkiiResort.Domain.Entities.Location;
using SkiiResort.Domain.Entities.Skipass;
using SkiiResort.Domain.Entities.Tariff;
using SkiiResort.Domain.Entities.Tariffication;
using SkiiResort.Domain.Entities.Visitor;
using SkiiResort.Domain.Entities.VisitorsAction;
using SkiiResort.Domain.Enums;

namespace SkiiResort.Domain;

public sealed class SkiiResortContext : DbContext
{
    public SkiiResortContext(DbContextOptions opts) : base(opts)
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
        modelBuilder.HasPostgresEnum<OperationType>();

        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}