using Microsoft.EntityFrameworkCore;
using SkiiResort.Domain;
using SkiiResort.Domain.Entities.Tariff;

namespace SkiiResort.Repository.Tariff;

internal sealed class TariffRepository : Repository<TariffRecord>, ITariffRepository
{
    public TariffRepository(SkiiResortContext context) : base(context)
    {
    }
}
