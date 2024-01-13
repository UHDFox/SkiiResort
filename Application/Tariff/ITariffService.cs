using Domain.Entities.Tariff;

namespace Application.Tariff;

public interface ITariffService
{
    Task<IReadOnlyCollection<GetTariffModel>> GetListAsync(int offset, int limit);
    Task<TariffRecord> GetByIdAsync(Guid id);
    Task<TariffRecord> AddAsync(AddTariffModel tariffModel);
    Task DeleteAsync(Guid id);
    Task<bool> UpdateAsync(UpdateTariffModel tariffModel);
}