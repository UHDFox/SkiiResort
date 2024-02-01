using Domain.Entities.Visitor;

namespace Application.Visitor;

public interface IVisitorService
{
    Task<VisitorRecord> AddAsync(AddVisitorModel model);
    Task<IReadOnlyCollection<GetVisitorModel>> GetListAsync(int offset, int limit);
    Task<GetVisitorModel> GetByIdAsync(Guid id);
    Task<bool> UpdateAsync(UpdateVisitorModel model);
    Task DeleteAsync(Guid id);
}