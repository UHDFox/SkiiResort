using AutoMapper;
using SkiiResort.Application.Location;
using SkiiResort.Application.Skipass;
using SkiiResort.Application.Tariff;
using SkiiResort.Application.Tariffication;
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
        CreateMap<SkipassModel, SkipassResponse>().ReverseMap();
        CreateMap<CreateSkipassRequest, SkipassModel>().ReverseMap();
        CreateMap<UpdateSkipassRequest, SkipassModel>().ReverseMap();

        CreateMap<TariffModel, TariffResponse>().ReverseMap();
        CreateMap<CreateTariffRequest, TariffModel>().ReverseMap();
        CreateMap<UpdateTariffRequest, TariffModel>().ReverseMap();

        CreateMap<VisitorModel, VisitorResponse>().ReverseMap();
        CreateMap<CreateVisitorRequest, VisitorModel>().ReverseMap();
        CreateMap<UpdateVisitorRequest, VisitorModel>().ReverseMap();

        CreateMap<VisitorActionsModel, VisitorActionsResponse>().ReverseMap();
        CreateMap<CreateVisitorActionsRequest, VisitorActionsModel>().ReverseMap();
        CreateMap<UpdateVisitorActionsRequest, VisitorActionsModel>().ReverseMap();

        CreateMap<LocationModel, LocationResponse>().ReverseMap();
        CreateMap<CreateLocationRequest, LocationModel>().ReverseMap();
        CreateMap<UpdateLocationRequest, LocationModel>().ReverseMap();

        CreateMap<TarifficationModel, TarifficationResponse>().ReverseMap();
        CreateMap<CreateTarifficationRequest, TarifficationModel>().ReverseMap();
        CreateMap<UpdateTarifficationRequest, TarifficationModel>().ReverseMap();


        CreateMap<TapSkipassRequest, AddVisitorActionsModel>().ReverseMap();
        CreateMap<DepositSkipassBalanceRequest, AddVisitorActionsModel>();

        /*CreateMap<GetUserModel, UserResponse>().ReverseMap();
        CreateMap<CreateUserRequest, AddUserModel>().ReverseMap();
        CreateMap<UpdateUserRequest, UpdateUserModel>().ReverseMap();
        CreateMap<LoginRequest, LoginModel>();
        CreateMap<RegisterRequest, RegisterModel>();*/
        CreateMap<CreateUserRequest, UserModel>()
            .ConstructUsing(src => new UserModel(
                Guid.NewGuid(),  // Generate a new ID or handle it as needed
                src.Name,
                src.Email,
                src.Password,
                src.Role,
                src.CreatedAt
            ));
        CreateMap<UpdateUserRequest, UserModel>()
            .ConstructUsing(src => new UserModel(
                src.Id,
                src.Name,
                src.Email,
                src.Password,
                src.Role,
                src.CreatedAt));
        CreateMap<LoginRequest, LoginModel>();
        CreateMap<RegisterRequest, RegisterModel>();
        CreateMap<UserModel, UserResponse>()
            .ConstructUsing(src => new UserResponse(
                src.Id,
                src.Name,
                src.Password,
                src.Email,
                src.Role,
                src.CreatedAt));
    }
}
