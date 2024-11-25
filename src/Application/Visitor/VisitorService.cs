using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using AutoMapper;
using SkiiResort.Application.Exceptions;
using SkiiResort.Domain.Entities.Visitor;
using SkiiResort.Repository.Visitor;

namespace SkiiResort.Application.Visitor;

internal sealed class VisitorService : Service<VisitorModel, VisitorRecord> ,IVisitorService
{
    private static readonly Regex passportRegex = new(@"\d{4}-\d{6}", RegexOptions.IgnoreCase | RegexOptions.Compiled);
    private static readonly Regex phoneNumberRegex = new(@"^\d{9,11}$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

    private readonly IMapper mapper;
    private readonly IVisitorRepository repository;

    public VisitorService(IMapper mapper, IVisitorRepository repository)
    : base(repository, mapper)
    {
        this.mapper = mapper;
        this.repository = repository;
    }

    public new async Task<Guid> AddAsync(VisitorModel model)
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

    public new async Task<VisitorModel> UpdateAsync(VisitorModel model)
    {
        var entity = await repository.GetByIdAsync(model.Id)
                     ?? throw new NotFoundException("visitor entity not found");

        if (model.Passport != null && !passportRegex.IsMatch(model.Passport))
        {
            throw new ValidationException("Validation error - check passport series and number");
        }

        if (!phoneNumberRegex.IsMatch(model.Phone))
        {
            throw new ValidationException("Validation error - check passport series and number");
        }


        mapper.Map(model, entity);
        repository.UpdateAsync(entity);
        await repository.SaveChangesAsync();

        return model;
    }
}
