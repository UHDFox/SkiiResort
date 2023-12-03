using Domain.Entities.Skipass;

namespace Application.Skipass;

public interface ISkipassService
{
    Task<IReadOnlyCollection<GetSkipassModel>> GetListAsync(int offset = 0, int limit = 150);
    Task<GetSkipassModel> GetByIdAsync(Guid id);
    Task<AddSkipassModel> AddAsync(AddSkipassModel skipassModel);
    Task<bool> UpdateAsync(UpdateSkipassModel skipassModel);
    Task<bool> DeleteAsync(DeleteSkipassModel skipassModel);
}