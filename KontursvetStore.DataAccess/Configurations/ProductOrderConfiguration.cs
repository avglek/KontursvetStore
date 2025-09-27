using KontursvetStore.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KontursvetStore.DataAccess.Configurations;

public class ProductOrderConfiguration: IEntityTypeConfiguration<ProductOrderEntity>
{
    public void Configure(EntityTypeBuilder<ProductOrderEntity> builder)
    {
        builder.HasKey(po => new { po.OrderId, po.ProductId });
        builder
            .HasOne(o => o.Order)
            .WithMany(po => po.ProductOrders)
            .HasForeignKey(po => po.OrderId);
        builder
            .HasOne(po => po.Product)
            .WithMany(p => p.ProductOrders)
            .HasForeignKey(po => po.ProductId);
    }
}