using AutoMapper;
using SkiiResort.Application.Location;
using SkiiResort.Application.Skipass;
using SkiiResort.Application.Tariff;
using SkiiResort.Application.Tariffication;
using SkiiResort.Application.User;
using SkiiResort.Application.Visitor;
using SkiiResort.Application.VisitorAction;
using SkiiResort.Domain.Entities.Location;
using SkiiResort.Domain.Entities.Skipass;
using SkiiResort.Domain.Entities.Tariff;
using SkiiResort.Domain.Entities.Tariffication;
using SkiiResort.Domain.Entities.User;
using SkiiResort.Domain.Entities.Visitor;
using SkiiResort.Domain.Entities.VisitorsAction;

namespace SkiiResort.Application.Infrastructure.Automapper;

public sealed class ApplicationProfile : Profile
{
    public ApplicationProfile()
    {
        CreateMap<SkipassModel, SkipassRecord>().ReverseMap();

        CreateMap<TariffModel, TariffRecord>().ReverseMap();

        CreateMap<VisitorModel, VisitorRecord>().ReverseMap();

        CreateMap<VisitorActionsModel, VisitorActionsRecord>().ReverseMap();
        CreateMap<AddVisitorActionsModel, VisitorActionsModel>();

        CreateMap<LocationModel, LocationRecord>().ReverseMap();

        CreateMap<TarifficationModel, TarifficationRecord>().ReverseMap();

        CreateMap<UserModel, UserRecord>()
            .ConstructUsing(src => new UserRecord(
                src.Name,
                src.Password,
                src.Email,
                src.Role,
                src.CreatedAt));

        CreateMap<UserRecord, UserModel>()
            .ConstructUsing(src => new UserModel(
                src.Id,
                src.Name,
                src.Email,
                src.PasswordHash,
                src.Role,
                src.CreatedAt));
    }
}
