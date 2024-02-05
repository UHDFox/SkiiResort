using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities.Skipass;

namespace Application.Skipass;

public interface ISkipassService
{
    Task<IReadOnlyCollection<GetSkipassModel>> GetListAsync(int offset, int limit);
    
    Task<GetSkipassModel> GetByIdAsync(Guid id);
    
    Task<Guid> AddAsync(AddSkipassModel skipassModel);
    
    Task<bool> UpdateAsync(UpdateSkipassModel skipassModel);
    
    Task DeleteAsync(Guid id);
}