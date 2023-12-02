using Domain.Entities.Skipass;
using Domain.Entities.Tariff;
using Domain.Entities.Visitor;
using Domain.Entities.VisitorsAction;
using Microsoft.EntityFrameworkCore;

namespace Domain;

public interface IHotelContext
{
    public DbSet<VisitorRecord> Visitors { get; }

    public DbSet<TariffRecord> Tariffs { get; }
    public DbSet<SkipassRecord> Skipasses { get; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}