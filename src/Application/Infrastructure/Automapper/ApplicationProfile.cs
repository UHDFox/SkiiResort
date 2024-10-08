using AutoMapper;
using SkiiResort.Application.Location.Models;
using SkiiResort.Application.Skipass;
using SkiiResort.Application.Tariff;
using SkiiResort.Application.Tariffication.Models;
using SkiiResort.Application.Visitor;
using SkiiResort.Application.VisitorAction;
using SkiiResort.Domain.Entities.Location;
using SkiiResort.Domain.Entities.Skipass;
using SkiiResort.Domain.Entities.Tariff;
using SkiiResort.Domain.Entities.Tariffication;
using SkiiResort.Domain.Entities.Visitor;
using SkiiResort.Domain.Entities.VisitorsAction;

namespace SkiiResort.Application.Infrastructure.Automapper;

public sealed class ApplicationProfile : Profile
{
    public ApplicationProfile()
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
