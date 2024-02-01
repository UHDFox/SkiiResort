using Domain.Entities.Skipass;
using Domain.Entities.Tariff;
using Domain.Entities.Visitor;
using Domain.Entities.VisitorsAction;
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
        modelBuilder.Entity<TariffRecord>()
            .HasMany(e => e.Skipasses)
            .WithOne(g => g.Tariff);
           // .HasForeignKey(e => e.TariffId);

        modelBuilder.Entity<VisitorRecord>()
            .HasMany(e => e.Skipasses)
            .WithOne(v => v.Visitor);

        modelBuilder.Entity<SkipassRecord>()
            .HasOne(t => t.Tariff)
            .WithMany(s => s.Skipasses);
        
        modelBuilder.Entity<SkipassRecord>()
            .HasOne(v => v.Visitor)
            .WithMany(s => s.Skipasses);

    
        
        modelBuilder.HasPostgresEnum<ActionType>();
        modelBuilder.HasPostgresEnum<Place>();
    }
}