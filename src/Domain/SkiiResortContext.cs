using Microsoft.EntityFrameworkCore;
using SkiiResort.Domain.Entities.Location;
using SkiiResort.Domain.Entities.Skipass;
using SkiiResort.Domain.Entities.Tariff;
using SkiiResort.Domain.Entities.Tariffication;
using SkiiResort.Domain.Entities.User;
using SkiiResort.Domain.Entities.Visitor;
using SkiiResort.Domain.Entities.VisitorsAction;
using SkiiResort.Domain.Enums;
using SkiiResort.Domain.Infrastructure;

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

    public DbSet<UserRecord> Users => Set<UserRecord>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum<OperationType>();
        modelBuilder.HasPostgresEnum<UserRole>();

        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder
            .Properties<DateTimeOffset>()
            .HaveConversion<DateTimeOffsetConverter>();
    }

}
