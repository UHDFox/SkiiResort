using Microsoft.EntityFrameworkCore;
using SkiiResort.Domain;
using SkiiResort.Domain.Entities.Tariffication;

namespace SkiiResort.Repository.Tariffication;

internal sealed class TarifficationRepository : Repository<TarifficationRecord>, ITarifficationRepository
{
    private readonly SkiiResortContext context;

    public TarifficationRepository(SkiiResortContext context) : base(context)
    {
        this.context = context;
    }

    public async Task<TarifficationRecord> GetByLocationAndTariffIdsAsync(Guid tariffId, Guid locationId)
    {
        return await context.Tariffications
            .AsNoTracking()
            .Where(t => t.TariffId == tariffId)
            .OrderByDescending(t => t.Price)
            .FirstAsync(t => t.LocationId == locationId);
    }
}
