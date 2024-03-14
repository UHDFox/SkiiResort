using Domain;

namespace Repository.DbContextRepository;

public sealed class SkiiResortContextRepository : IDbContextRepository<SkiiResortContext>
{
    private readonly SkiiResortContext context;

    public SkiiResortContextRepository(SkiiResortContext context)
    {
        this.context = context;
    }
    public SkiiResortContext GetDbContext()
    {
        return context;
    }
}