using Application.Exceptions;
using AutoMapper;
using Domain.Entities.Skipass;
using Repository.Skipass;

namespace Application.Skipass;

internal sealed class SkipassService : ISkipassService
{
    private readonly IMapper mapper;
    private readonly ISkipassRepository repository;

    public SkipassService(IMapper mapper, ISkipassRepository repository)
    {
        this.mapper = mapper;
        this.repository = repository;
    }

    public async Task<IReadOnlyCollection<GetSkipassModel>> GetListAsync(int offset, int limit)
    {
        return mapper.Map<IReadOnlyCollection<GetSkipassModel>>(await repository.GetListAsync(offset, limit));
    }

    public async Task<GetSkipassModel> GetByIdAsync(Guid skipassId)
    {
        var result = await repository.GetByIdAsync(skipassId) ?? throw new NotFoundException();
        return mapper.Map<GetSkipassModel>(result);
    }

    public async Task<Guid> AddAsync(AddSkipassModel skipassModel)
    {
        var result = await repository.AddAsync(mapper.Map<SkipassRecord>(skipassModel));
        return result;
    }

    public async Task<bool> UpdateAsync(UpdateSkipassModel skipassModel)
    {
        mapper.Map<SkipassRecord>(await GetByIdAsync(skipassModel.Id));
        return await repository.UpdateAsync(mapper.Map<SkipassRecord>(skipassModel));
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        await GetByIdAsync(id);
        return await repository.DeleteAsync(id);
    }
}