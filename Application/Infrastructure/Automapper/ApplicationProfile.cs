
using Application.Skipass;
using Application.Tariff;
using Application.Visitor;
using AutoMapper;
using Domain.Entities.Skipass;
using Domain.Entities.Tariff;
using Domain.Entities.Visitor;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Application.Infrastructure.Automapper;

public sealed class ApplicationProfile : Profile
{
    public ApplicationProfile()
    {
        CreateMap<VisitorDto, VisitorRecord>().ReverseMap();
        CreateMap<TariffDto, TariffRecord>().ReverseMap();
        CreateMap<AddSkipassModel, SkipassRecord>().ReverseMap();
        CreateMap<UpdateSkipassModel, SkipassRecord>().ReverseMap();
        CreateMap<GetSkipassModel, SkipassRecord>().ReverseMap();
        CreateMap<TariffRecord, GetTariffModel>().ReverseMap();
        CreateMap<EntityEntry<TariffRecord>, AddTariffModel>()
            .ConstructUsing(x => new AddTariffModel(x.Entity.Name));
    }
}