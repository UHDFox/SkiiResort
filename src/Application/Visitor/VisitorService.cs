using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using AutoMapper;
using SkiiResort.Application.Exceptions;
using SkiiResort.Domain.Entities.Visitor;
using SkiiResort.Repository.Visitor;

namespace SkiiResort.Application.Visitor;

internal sealed class VisitorService : IVisitorService
{
    private static readonly Regex passportRegex = new(@"\d{4}-\d{6}", RegexOptions.IgnoreCase | RegexOptions.Compiled);
    private static readonly Regex phoneNumberRegex = new(@"^\d{9,11}$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

    private readonly IMapper mapper;
    private readonly IVisitorRepository repository;

    public VisitorService(IMapper mapper, IVisitorRepository repository)
    {
        this.mapper = mapper;
        this.repository = repository;
    }

    public async Task<Guid> AddAsync(AddVisitorModel model)
    {
        var record = mapper.Map<VisitorRecord>(model);
        if (!passportRegex.IsMatch(record.Passport))
        {
            throw new ValidationException("Validation error - check passport series and number");
        }

        if (!phoneNumberRegex.IsMatch(record.Phone))
        {
            throw new ValidationException("Validation error - check if phone number's entered correctly");
        }

        return await repository.AddAsync(record);
    }

    public async Task<IReadOnlyCollection<GetVisitorModel>> GetListAsync(int offset, int limit)
    {
        var totalAmount = await repository.GetTotalAmountAsync();

        return mapper.Map<IReadOnlyCollection<GetVisitorModel>>(await repository.GetAllAsync(offset, limit));
    }

    public async Task<int> GetTotalAmountAsync() => await repository.GetTotalAmountAsync();

    public async Task<GetVisitorModel> GetByIdAsync(Guid id)
    {
        var entity = await repository.GetByIdAsync(id) ?? throw new NotFoundException();
        return mapper.Map<GetVisitorModel>(entity);
    }

    public async Task<UpdateVisitorModel> UpdateAsync(UpdateVisitorModel model)
    {
        var entity = await repository.GetByIdAsync(model.Id)
                     ?? throw new NotFoundException("visitor entity not found");

        if (!passportRegex.IsMatch(model.Passport))
        {
            throw new ValidationException("Validation error - check passport series and number");
        }

        if (!phoneNumberRegex.IsMatch(model.Phone))
        {
            throw new ValidationException("Validation error - check passport series and number");
        }


        mapper.Map(model, entity);
        repository.Update(entity);
        await repository.SaveChangesAsync();

        return model;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        await GetByIdAsync(id);
        return await repository.DeleteAsync(id);
    }
}
