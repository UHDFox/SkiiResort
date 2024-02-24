using Application.Location.Models;

namespace Application.Location;

public interface ILocationService
{
    Task<IReadOnlyCollection<GetLocationModel>> GetAllAsync(int offset, int limit);

    Task<GetLocationModel> GetByIdAsync(Guid id);

    Task<Guid> AddAsync(AddLocationModel model);

    Task<bool> UpdateAsync(UpdateLocationModel model);

    Task<bool> DeleteAsync(Guid id);
}