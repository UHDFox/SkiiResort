using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkiiResort.Domain.Entities.User;
using SkiiResort.Domain.Entities.Visitor;

namespace SkiiResort.Domain.EntitiesConfiguration;

public sealed class UserConfiguration : IEntityTypeConfiguration<UserRecord>
{
    public void Configure(EntityTypeBuilder<UserRecord> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .HasOne(e => e.Visitor)
            .WithOne(v => v.User)
            .HasForeignKey<UserRecord>(e => e.VisitorId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
