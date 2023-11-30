using AutoMapper;
using Application.Entities.Skipass;
using Application.Entities.Tariff;
using Application.Entities.Visitor;
using Domain.Entities.SkipassEntity;
using Domain.Entities.Tariff;
using Domain.Entities.Visitor;

namespace Application.Infrastructure.Automapper;

public sealed class ApplicationProfile : Profile
{
    public ApplicationProfile()
    {
        CreateMap<VisitorDto, Visitor>().ReverseMap();
        CreateMap<TariffDto, Tariff>().ReverseMap();
        CreateMap<SkipassDto, Skipass>().ReverseMap();
    }
}