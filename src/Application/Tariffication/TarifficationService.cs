using AutoMapper;
using SkiiResort.Domain.Entities.Tariffication;
using SkiiResort.Repository.Tariffication;

namespace SkiiResort.Application.Tariffication;

public sealed class TarifficationService : Service<TarifficationModel, TarifficationRecord>, ITarifficationService
{
    private readonly IMapper mapper;
    private readonly ITarifficationRepository repository;

    public TarifficationService(ITarifficationRepository repository, IMapper mapper)
    : base(repository, mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }
}
