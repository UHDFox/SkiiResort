using Application.Location.Models;
using Application.Skipass;
using Application.Tariff;
using Application.Tariffication.Models;
using Application.Visitor;
using Application.VisitorAction;
using AutoMapper;
using Domain.Entities.Location;
using Domain.Entities.Skipass;
using Domain.Entities.Tariff;
using Domain.Entities.Tariffication;
using Domain.Entities.Visitor;
using Domain.Entities.VisitorsAction;

namespace Tests.AutomapperProfiles;

public sealed class TestMapperProfile : Profile
{
    public TestMapperProfile()
    {
        CreateMap<AddSkipassModel, SkipassRecord>().ReverseMap();
        CreateMap<UpdateSkipassModel, SkipassRecord>().ReverseMap();
        CreateMap<GetSkipassModel, SkipassRecord>().ReverseMap();

        CreateMap<TariffRecord, GetTariffModel>().ReverseMap();
        CreateMap<AddTariffModel, TariffRecord>().ReverseMap();
        CreateMap<UpdateTariffModel, TariffRecord>().ReverseMap();
        

        CreateMap<VisitorRecord, GetVisitorModel>().ReverseMap();
        CreateMap<AddVisitorModel, VisitorRecord>().ReverseMap();
        CreateMap<UpdateVisitorModel, VisitorRecord>().ReverseMap();

        CreateMap<AddVisitorActionsModel, VisitorActionsRecord>();
        CreateMap<GetVisitorActionsModel, VisitorActionsRecord>().ReverseMap();
        CreateMap<UpdateVisitorActionsModel, VisitorActionsRecord>().ReverseMap();

        CreateMap<GetLocationModel, LocationRecord>().ReverseMap();
        CreateMap<AddLocationModel, LocationRecord>().ReverseMap();
        CreateMap<UpdateLocationModel, LocationRecord>().ReverseMap();

        CreateMap<GetTarifficationModel, TarifficationRecord>().ReverseMap();
        CreateMap<AddTarifficationModel, TarifficationRecord>().ReverseMap();
        CreateMap<UpdateTarifficationModel, TarifficationRecord>().ReverseMap();
    }
}

