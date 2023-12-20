using Domain.Entities.Skipass;
using Domain.Entities.Tariff;
using Domain.Entities.Visitor;
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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }
}