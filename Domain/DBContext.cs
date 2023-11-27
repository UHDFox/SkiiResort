using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain;

public class DBContext : DbContext, IDBContext
{
    public DBContext(DbContextOptions opts) : base(opts)
    {
        
    }

    public DbSet<Visitor> Visitors { get; set; }
    public DbSet<Tariff> Tariffs { get; }
    public DbSet<Skipass> Skipasses { get; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }
    
}
