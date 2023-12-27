using Domain.Entities.Tariff;

namespace Application.Tariff;

public interface ITariffService
{
    Task<AddTariffModel> AddAsync(AddTariffModel tariffModel);
    Task<IReadOnlyCollection<GetTariffModel>> GetListAsync(int? offset = 0, int? limit = 150);
}