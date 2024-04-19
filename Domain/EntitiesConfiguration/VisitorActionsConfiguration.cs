using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkiiResort.Domain.Entities.VisitorsAction;

namespace SkiiResort.Domain.EntitiesConfiguration;

public sealed class VisitorActionsConfiguration : IEntityTypeConfiguration<VisitorActionsRecord>
{
    public void Configure(EntityTypeBuilder<VisitorActionsRecord> builder)
    {
        builder.HasKey(va => va.Id);

        builder.HasOne(s => s.Skipass).WithMany(va => va.VisitorActions);
    }
}
