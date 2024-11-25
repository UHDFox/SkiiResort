using AutoMapper;
using SkiiResort.Application.Exceptions;
using SkiiResort.Domain.Entities.Skipass;
using SkiiResort.Repository.Skipass;

namespace SkiiResort.Application.Skipass;

internal sealed class SkipassService : Service<SkipassModel, SkipassRecord> ,ISkipassService
{
    private readonly IMapper mapper;
    private readonly ISkipassRepository repository;

    public SkipassService(IMapper mapper, ISkipassRepository repository)
    : base(repository, mapper)
    {
        this.mapper = mapper;
        this.repository = repository;
    }
}
