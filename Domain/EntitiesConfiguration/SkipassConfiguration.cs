using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.EntitiesConfiguration;

public class SkipassConfiguration : IEntityTypeConfiguration<Skipass>
{
    public void Configure(EntityTypeBuilder<Skipass> builder)
    {
        builder.HasKey(e => e.Id);
        builder.HasOne(t => t.Tariff);
        builder.HasOne(v => v.Visitor);

    }
}