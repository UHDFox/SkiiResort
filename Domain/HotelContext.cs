using Domain.Entities.Skipass;
using Domain.Entities.Tariff;
using Domain.Entities.Visitor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Domain;

public sealed class HotelContext : DbContext, IHotelContext
{
    public HotelContext(DbContextOptions opts) : base(opts)
    {
    }

    public DbSet<VisitorRecord> Visitors
    {
        get => Set<VisitorRecord>();
    }
    public DbSet<TariffRecord> Tariffs => Set<TariffRecord>();
    public DbSet<SkipassRecord> Skipasses => Set<SkipassRecord>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }
}