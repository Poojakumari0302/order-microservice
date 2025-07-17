using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Orders;

public class OrderEntityConfiguration : EntityTypeConfiguration<Order>
{
    public override void Configure(EntityTypeBuilder<Order> orderConfiguration)
    {
        orderConfiguration.ToTable("Orders");
        orderConfiguration.HasKey(b => b.Id);
        orderConfiguration.Ignore(b => b.DomainEvents);
        orderConfiguration.OwnsOne(b => b.OrderNumber, orderNumber =>
            {
                orderNumber.Property(o => o.Value)
                           .IsRequired()
                           .HasColumnName("OrderNumber");
            });
    }
}