using AutoMapper;
using SkiiResort.Application.Exceptions;
using SkiiResort.Domain.Entities.Tariff;
using SkiiResort.Repository.Tariff;

namespace SkiiResort.Application.Tariff;

internal sealed class TariffService : Service<TariffModel, TariffRecord>, ITariffService
{
    private readonly IMapper mapper;
    private readonly ITariffRepository repository;

    public TariffService(IMapper mapper, ITariffRepository repository)
    : base(repository, mapper)
    {
        this.mapper = mapper;
        this.repository = repository;
    }


}
