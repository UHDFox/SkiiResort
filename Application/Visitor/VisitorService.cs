using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Application.Exceptions;
using AutoMapper;
using Domain;
using Domain.Entities.Visitor;
using Microsoft.EntityFrameworkCore;

namespace Application.Visitor;

internal sealed class VisitorService : IVisitorService
{
    private readonly HotelContext context;
    private readonly IMapper mapper;
    
    public VisitorService(HotelContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    private static Regex passportRegex = new Regex(@"\d{4}-\d{6}");
    public async Task<VisitorRecord> AddAsync(AddVisitorModel model)
    {
        var result = await context.Visitors.AddAsync(mapper.Map<VisitorRecord>(model));

        if (!passportRegex.IsMatch(result.Entity.Passport))
        {
            throw new ValidationException("Validation error - check passport series and number");
        }
        await context.SaveChangesAsync(); 
        return mapper.Map<VisitorRecord>(result.Entity);
    }
    public async Task<IReadOnlyCollection<GetVisitorModel>> GetListAsync(int offset, int limit)
    {
        var result = await context.Visitors.Skip(offset).Take(limit).ToListAsync();
        return mapper.Map<IReadOnlyCollection<GetVisitorModel>>(result);
    }

    public async Task<VisitorRecord> GetByIdAsync(Guid id)
    {
        var entity = await context.Visitors.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id) ?? throw new NotFoundException();
        return entity;
    }

    public async Task<bool> UpdateAsync(UpdateVisitorModel model)
    {
        var visitor = await GetByIdAsync(model.Id);
        context.Visitors.Update(mapper.Map(model, visitor));
        return await context.SaveChangesAsync() > 0;
    }

    public async Task DeleteAsync(Guid id)
    {
        var visitor = await GetByIdAsync(id) ?? throw new NotFoundException();
        context.Visitors.Remove(mapper.Map<VisitorRecord>(visitor));
        var result = await context.SaveChangesAsync();
    }
}