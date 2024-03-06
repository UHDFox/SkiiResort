using Domain;

namespace Repository.DbContextRepository;

public sealed class HotelContextRepository : IDbContextRepository<HotelContext>
{
    private readonly HotelContext context;

    public HotelContextRepository(HotelContext context)
    {
        this.context = context;
    }
    public HotelContext GetDbContext()
    {
        return context;
    }
}