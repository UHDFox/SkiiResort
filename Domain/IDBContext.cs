using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain;

public interface IDBContext
{
   public DbSet<Visitor> Visitors { get; set; }

   public DbSet<Tariff> Tariffs {get;}
   public DbSet<Skipass> Skipasses { get; }
   
   public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

}