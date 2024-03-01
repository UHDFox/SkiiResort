using Application.Location.Models;
using Application.Skipass;
using Application.Tariff;
using Application.Tariffication.Models;
using Application.Visitor;
using Application.VisitorAction;
using AutoMapper;
using Web.Contracts.Location;
using Web.Contracts.Location.Requests;
using Web.Contracts.Skipass;
using Web.Contracts.Skipass.Requests;
using Web.Contracts.Tariff;
using Web.Contracts.Tariff.Requests;
using Web.Contracts.Tariffication;
using Web.Contracts.Tariffication.Requests;
using Web.Contracts.Visitor;
using Web.Contracts.Visitor.Requests;
using Web.Contracts.VisitorActions;
using Web.Contracts.VisitorActions.Requests;

namespace Web.Infrastructure.Automapper;

public sealed class WebProfile : Profile
{
    public WebProfile()
    {
        CreateMap<GetSkipassModel, SkipassResponse>().ReverseMap();
        CreateMap<CreateSkipassRequest, AddSkipassModel>().ReverseMap();
        CreateMap<UpdateSkipassRequest, UpdateSkipassModel>().ReverseMap();

        CreateMap<GetTariffModel, TariffResponse>().ReverseMap();
        CreateMap<CreateTariffRequest, AddTariffModel>().ReverseMap();
        CreateMap<UpdateTariffRequest, UpdateTariffModel>().ReverseMap();

        CreateMap<GetVisitorModel, VisitorResponse>().ReverseMap();
        CreateMap<CreateVisitorRequest, AddVisitorModel>().ReverseMap();
        CreateMap<UpdateVisitorRequest, UpdateVisitorModel>().ReverseMap();

        CreateMap<GetVisitorActionsModel, VisitorActionsResponse>().ReverseMap();
        CreateMap<CreateVisitorActionsRequest, AddVisitorActionsModel>().ReverseMap();
        CreateMap<UpdateVisitorActionsRequest, UpdateVisitorActionsModel>().ReverseMap();

        CreateMap<GetLocationModel, LocationResponse>().ReverseMap();
        CreateMap<CreateLocationRequest, AddLocationModel>().ReverseMap();
        CreateMap<UpdateLocationRequest, UpdateLocationModel>().ReverseMap();

        CreateMap<GetTarifficationModel, TarifficationResponse>().ReverseMap();
        CreateMap<CreateTarifficationRequest, AddTarifficationModel>().ReverseMap();
        CreateMap<UpdateTarifficationRequest, UpdateTarifficationModel>().ReverseMap();
        CreateMap<TapSkipassRequest, AddVisitorActionsModel>().ReverseMap();
        CreateMap<DepositSkipassBalanceRequest, AddVisitorActionsModel>();
    }
}