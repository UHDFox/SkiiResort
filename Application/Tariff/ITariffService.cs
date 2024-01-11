using Domain.Entities.Tariff;

namespace Application.Tariff;

public interface ITariffService
{
    Task<IReadOnlyCollection<GetTariffModel>> GetListAsync(int? offset = 0, int? limit = 150);
    Task<GetTariffModel> GetByIdAsync(Guid id);
    Task<TariffRecord> AddAsync(AddTariffModel tariffModel);
    Task DeleteAsync(Guid id);
    Task<bool> UpdateAsync(Guid id, UpdateTariffModel tariffModel);
}