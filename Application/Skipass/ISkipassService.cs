using Domain.Entities.Skipass;

namespace Application.Skipass;

public interface ISkipassService
{
    Task<IReadOnlyCollection<GetSkipassModel>> GetListAsync(int offset = 0, int limit = 150);
    Task<SkipassRecord> GetByIdAsync(Guid id);
    Task<SkipassRecord> AddAsync(AddSkipassModel skipassModel);
    Task<bool> UpdateAsync(UpdateSkipassModel skipassModel);
    Task<bool> DeleteAsync(Guid id);
}