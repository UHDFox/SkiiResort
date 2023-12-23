using System.Collections.ObjectModel;
using Application.Tariff;
using AutoMapper;
using Domain.Entities.Tariff;
using Web.Contracts.Tariff;

namespace Web.Infrastructure;

public class WebProfile : Profile
{
    public WebProfile()
    {
        CreateMap<TariffResponse, GetTariffModel>().ReverseMap();
        CreateMap<TariffRecord, GetTariffModel>().ReverseMap();
        CreateMap<IReadOnlyCollection<TariffResponse>, IReadOnlyCollection<GetTariffModel>>().ReverseMap();
        CreateMap<ReadOnlyCollection<GetTariffModel>, List<TariffRecord>>();
    }
}