using AutoMapper;
using SkiiResort.Application.Exceptions;
using SkiiResort.Domain.Entities.Skipass;
using SkiiResort.Repository.Skipass;

namespace SkiiResort.Application.Skipass;

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
        var totalAmount = await repository.GetTotalAmountAsync();

        return mapper.Map<IReadOnlyCollection<GetSkipassModel>>(await repository.GetAllAsync(offset, limit));
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

    public async Task<UpdateSkipassModel> UpdateAsync(UpdateSkipassModel skipassModel)
    {
        var entity = await repository.GetByIdAsync(skipassModel.Id)
                     ?? throw new NotFoundException();

        mapper.Map(skipassModel, entity);

        repository.Update(entity);
        await repository.SaveChangesAsync();

        return skipassModel;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        await GetByIdAsync(id);
        return await repository.DeleteAsync(id);
    }
}
