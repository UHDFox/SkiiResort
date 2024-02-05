namespace Application.Tariff;

public interface ITariffService
{
    Task<IReadOnlyCollection<GetTariffModel>> GetListAsync(int offset, int limit);
    
    Task<GetTariffModel> GetByIdAsync(Guid id);
    
    Task<Guid> AddAsync(AddTariffModel tariffModel);
    
    Task DeleteAsync(Guid id);
    
    Task<bool> UpdateAsync(UpdateTariffModel tariffModel);
}