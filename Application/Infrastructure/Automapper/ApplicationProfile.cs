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
        CreateMap<AddSkipassModel, SkipassRecord>().ReverseMap();
        CreateMap<UpdateSkipassModel, SkipassRecord>().ReverseMap();
        CreateMap<GetSkipassModel, SkipassRecord>().ReverseMap();

        CreateMap<TariffRecord, GetTariffModel>().ReverseMap();
        CreateMap<AddTariffModel, TariffRecord>().ReverseMap();
        CreateMap<UpdateTariffModel, TariffRecord>().ReverseMap();
        CreateMap<GetTariffModel, UpdateTariffModel>().ReverseMap();
        CreateMap<EntityEntry<TariffRecord>, AddTariffModel>()
            .ConstructUsing(x => new AddTariffModel(x.Entity.Name));

        CreateMap<VisitorRecord, GetVisitorModel>().ReverseMap();
        CreateMap<VisitorDto, AddVisitorModel>().ReverseMap();
    }
}