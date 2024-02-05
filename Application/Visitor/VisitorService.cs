using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Application.Exceptions;
using AutoMapper;
using Domain;
using Domain.Entities.Visitor;
using Repository.Visitor;

namespace Application.Visitor;

internal sealed class VisitorService : IVisitorService
{
    private static readonly Regex passportRegex = new(@"\d{4}-\d{6}");
    private readonly HotelContext context;
    private readonly IMapper mapper;
    private readonly IVisitorRepository repository;

    public VisitorService(HotelContext context, IMapper mapper, IVisitorRepository repository)
    {
        this.context = context;
        this.mapper = mapper;
        this.repository = repository;
    }

    public async Task<Guid> AddAsync(AddVisitorModel model)
    {
        var record = mapper.Map<VisitorRecord>(model);
        if (!passportRegex.IsMatch(record.Passport))
            throw new ValidationException("Validation error - check passport series and number");

        return await repository.AddAsync(record);
    }

    public async Task<IReadOnlyCollection<GetVisitorModel>> GetListAsync(int offset, int limit)
    {
        return mapper.Map<IReadOnlyCollection<GetVisitorModel>>(await repository.GetListAsync(offset, limit));
    }

    public async Task<GetVisitorModel> GetByIdAsync(Guid id)
    {
        var entity = await repository.GetByIdAsync(id) ?? throw new NotFoundException();
        return mapper.Map<GetVisitorModel>(entity);
    }

    public async Task<bool> UpdateAsync(UpdateVisitorModel model)
    {
        await GetByIdAsync(model.Id); //to check if such record exists in db
        return await repository.UpdateAsync(mapper.Map<VisitorRecord>(model));
    }

    public async Task DeleteAsync(Guid id)
    {
        await GetByIdAsync(id);
        await repository.DeleteAsync(id);
    }
}