using Application.Exceptions;
using AutoMapper;
using Domain;
using Domain.Entities.Tariff;
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

    public async Task<VisitorRecord> AddAsync(AddVisitorModel model)
    {
        var result = await context.Visitors.AddAsync(mapper.Map<VisitorRecord>(model));
        await context.SaveChangesAsync(); 
        return mapper.Map<VisitorRecord>(result.Entity);
    }
    public async Task<IReadOnlyCollection<GetVisitorModel>> GetListAsync(int? offset, int? limit)
    {
        var result = await context.Visitors.Skip((int)offset!).Take((int)limit!).ToListAsync();
        return mapper.Map<IReadOnlyCollection<GetVisitorModel>>(result);
    }

    public async Task<GetVisitorModel> GetByIdAsync(Guid id)
    {
        var entity = await context.Visitors.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id) ?? throw new NotFoundException();
        return mapper.Map<GetVisitorModel>(entity);
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateVisitorModel model)
    {
        await GetByIdAsync(id); //to check if such record exists in db
        context.Visitors.Update(mapper.Map<VisitorRecord>(model));
        return await context.SaveChangesAsync() > 0;
    }

    public async Task DeleteAsync(Guid id)
    {
        var visitor = await GetByIdAsync(id) ?? throw new NotFoundException();
        context.Visitors.Remove(mapper.Map<VisitorRecord>(visitor));
        var result = await context.SaveChangesAsync();
    }
}