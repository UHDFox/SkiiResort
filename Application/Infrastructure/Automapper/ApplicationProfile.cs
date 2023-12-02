using Application.Entities.Skipass;
using Application.Entities.Tariff;
using Application.Entities.Visitor;
using AutoMapper;
using Domain.Entities.Skipass;
using Domain.Entities.Tariff;
using Domain.Entities.Visitor;

namespace Application.Infrastructure.Automapper;

public sealed class ApplicationProfile : Profile
{
    public ApplicationProfile()
    {
        CreateMap<VisitorDto, VisitorRecord>().ReverseMap();
        CreateMap<TariffDto, TariffRecord>().ReverseMap();
        CreateMap<SkipassDto, SkipassRecord>().ReverseMap();
    }
}