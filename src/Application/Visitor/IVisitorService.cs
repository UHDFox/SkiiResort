namespace SkiiResort.Application.Visitor;

public interface IVisitorService
{
    Task<Guid> AddAsync(AddVisitorModel model);

    Task<IReadOnlyCollection<GetVisitorModel>> GetListAsync(int offset, int limit);

    Task<int> GetTotalAmountAsync();

    Task<GetVisitorModel> GetByIdAsync(Guid id);

    Task<UpdateVisitorModel> UpdateAsync(UpdateVisitorModel model);

    Task<bool> DeleteAsync(Guid id);
}
