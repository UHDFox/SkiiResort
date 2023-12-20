using Application.Skipass;
using Application.Tariff;
using Application.Visitor;
using AutoMapper;
using Domain.Entities.Skipass;
using Domain.Entities.Tariff;
using Domain.Entities.Visitor;

namespace Application.Infrastructure.Automapper;

public sealed class ApplicationProfile : Profile
{
    /*public IServiceCollection ConfigureAutomapper(ApplicationProfile profile)
    {
        var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
        var mapper = mapperConfiguration.CreateMapper();

    }*/

    public ApplicationProfile()
    {
        CreateMap<VisitorDto, VisitorRecord>().ReverseMap();
        CreateMap<TariffDto, TariffRecord>().ReverseMap();
        CreateMap<AddSkipassModel, SkipassRecord>().ReverseMap();
        CreateMap<UpdateSkipassModel, SkipassRecord>().ReverseMap();
        CreateMap<GetSkipassModel, SkipassRecord>().ReverseMap();
        CreateMap<TariffRecord, AddTariffModel>().ReverseMap();
        CreateMap<AddTariffModel, TariffRecord>().ReverseMap();
    }
}