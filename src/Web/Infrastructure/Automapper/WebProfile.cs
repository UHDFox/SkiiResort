using AutoMapper;
using SkiiResort.Application.Location.Models;
using SkiiResort.Application.Skipass;
using SkiiResort.Application.Tariff;
using SkiiResort.Application.Tariffication.Models;
using SkiiResort.Application.User;
using SkiiResort.Application.Visitor;
using SkiiResort.Application.VisitorAction;
using SkiiResort.Web.Contracts.Location;
using SkiiResort.Web.Contracts.Location.Requests;
using SkiiResort.Web.Contracts.Skipass;
using SkiiResort.Web.Contracts.Skipass.Requests;
using SkiiResort.Web.Contracts.Tariff;
using SkiiResort.Web.Contracts.Tariff.Requests;
using SkiiResort.Web.Contracts.Tariffication;
using SkiiResort.Web.Contracts.Tariffication.Requests;
using SkiiResort.Web.Contracts.User;
using SkiiResort.Web.Contracts.User.Requests;
using SkiiResort.Web.Contracts.Visitor;
using SkiiResort.Web.Contracts.Visitor.Requests;
using SkiiResort.Web.Contracts.VisitorActions;
using SkiiResort.Web.Contracts.VisitorActions.Requests;

namespace SkiiResort.Web.Infrastructure.Automapper;

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

        CreateMap<GetUserModel, UserResponse>().ReverseMap();
        CreateMap<CreateUserRequest, AddUserModel>().ReverseMap();
        CreateMap<UpdateUserRequest, UpdateUserModel>().ReverseMap();
        CreateMap<LoginRequest, LoginModel>();
        CreateMap<RegisterRequest, RegisterModel>();
    }
}
