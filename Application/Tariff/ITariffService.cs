namespace Application.Tariff;

public interface ITariffService
{
    Task<AddTariffModel> AddAsync(AddTariffModel tariffModel);
}