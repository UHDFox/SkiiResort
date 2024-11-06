using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkiiResort.Domain.Entities.User;

namespace SkiiResort.Domain.EntitiesConfiguration;

public sealed class UserConfiguration : IEntityTypeConfiguration<UserRecord>
{
    public void Configure(EntityTypeBuilder<UserRecord> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .HasIndex(e => e.Email)
            .IsUnique();
    }
}
