using Application.Entities;
using Application.Entities.Tariff;
using Application.Entities.Visitor;
using AutoMapper;
using Domain;
using Domain.Entities;

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