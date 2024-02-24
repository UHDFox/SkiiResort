namespace Application.Skipass;

public interface ISkipassService
{
    Task<IReadOnlyCollection<GetSkipassModel>> GetListAsync(int offset, int limit);

    Task<GetSkipassModel> GetByIdAsync(Guid id);

    Task<Guid> AddAsync(AddSkipassModel skipassModel);

    Task<bool> UpdateAsync(UpdateSkipassModel skipassModel);

    Task<bool> DeleteAsync(Guid id);
}