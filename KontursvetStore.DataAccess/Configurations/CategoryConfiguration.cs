using KontursvetStore.Core;
using KontursvetStore.Core.Constants;
using KontursvetStore.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static System.DateTime;

namespace KontursvetStore.DataAccess.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<CategoryEntity>
{
    public void Configure(EntityTypeBuilder<CategoryEntity> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Name).IsRequired().HasMaxLength(StoreAppConstants.MAX_NAME_LENGTH);
    }
}