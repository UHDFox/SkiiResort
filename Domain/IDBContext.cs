using Domain.Entities.SkipassEntity;
using Domain.Entities.Tariff;
using Domain.Entities.Visitor;
using Microsoft.EntityFrameworkCore;

namespace Domain;

public interface IDBContext
{
    public DbSet<Visitor> Visitors { get; set; }

    public DbSet<Tariff> Tariffs { get; }
    public DbSet<SkipassRecord> Skipasses { get; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}