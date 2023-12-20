using Application.Exceptions;
using AutoMapper;
using Domain;
using Domain.Entities.Skipass;
using Microsoft.EntityFrameworkCore;

namespace Application.Skipass;

internal class SkipassService : ISkipassService
{
    private readonly HotelContext context;
    private readonly IMapper mapper;


    public async Task<IReadOnlyCollection<GetSkipassModel>> GetListAsync(int offset = 0, int limit = 150)
    {
        //await context.Boxes.FirstOrDefaultAsync(box => box.Id == id, cancellationToken);
        return mapper.Map<IReadOnlyCollection<GetSkipassModel>>(await context.Skipasses.Take(limit).ToListAsync());
    }

    public async Task<GetSkipassModel> GetByIdAsync(Guid skipassId)
    {
        return mapper.Map<GetSkipassModel>(await context.Skipasses.FindAsync(skipassId)) ??
               throw new NotFoundException("Skipass not found");
    }

    public async Task<AddSkipassModel> AddAsync(AddSkipassModel skipassModel)
    {
        return mapper.Map<AddSkipassModel>(await context.Skipasses.AddAsync(mapper.Map<SkipassRecord>(skipassModel)));
    }

    public async Task<bool> UpdateAsync(UpdateSkipassModel skipassModel)
    {
        var record = context.Skipasses.FirstOrDefault(record => record.Id == skipassModel.Id) ??
                     throw new NotFoundException("Skipass not found");
        mapper.Map<SkipassRecord>(record);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(DeleteSkipassModel skipassModel)
    {
        var record = context.Skipasses.FirstOrDefault(record => record.Id == skipassModel.Id) ??
                     throw new NotFoundException("Skipass not found");
        context.Skipasses.Remove(record);
        return await context.SaveChangesAsync() > 0;
    }
}