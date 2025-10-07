using KontursvetStore.Core.Constants;
using KontursvetStore.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KontursvetStore.DataAccess.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(StoreAppConstants.MAX_NAME_LENGTH);

        builder
            .HasMany(x => x.Orders)
            .WithOne(x => x.User);
    }
}