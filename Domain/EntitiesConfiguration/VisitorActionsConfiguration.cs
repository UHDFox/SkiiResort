using Domain.Entities.VisitorsAction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.EntitiesConfiguration;

public sealed class VisitorActionsConfiguration : IEntityTypeConfiguration<VisitorActionsRecord>
{
    public void Configure(EntityTypeBuilder<VisitorActionsRecord> builder)
    {
        builder.HasKey(va => va.Id);
        
        builder.HasOne(s => s.Skipass);
    }
}