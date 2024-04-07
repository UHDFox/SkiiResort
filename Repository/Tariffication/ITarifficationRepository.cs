using Domain.Entities.Tariffication;

namespace Repository.Tariffication;

public interface ITarifficationRepository : IRepository<TarifficationRecord>
{
    public Task<TarifficationRecord> GetByLocationAndTariffIdsAsync(Guid tariffId, Guid locationId);
}