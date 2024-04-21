using SkiiResort.Domain.Entities.Tariffication;

namespace SkiiResort.Repository.Tariffication;

public interface ITarifficationRepository : IRepository<TarifficationRecord>
{
    public Task<TarifficationRecord> GetByLocationAndTariffIdsAsync(Guid tariffId, Guid locationId);
}
