using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Orders;

public class OrderEntityConfiguration : EntityTypeConfiguration<Order>
{
    public override void Configure(EntityTypeBuilder<Order> orderConfiguration)
    {
        orderConfiguration.ToTable("Orders");
        orderConfiguration.HasKey(b => b.Id);
        orderConfiguration.Property(o => o.CreatedAtUtc)
               .IsRequired();
        orderConfiguration.Property(o => o.Status)
               .HasConversion<string>() // Store enum as string
               .IsRequired();
        //Index on CreatedAtUtc for faster time-range queries
        orderConfiguration.HasIndex(o => o.CreatedAtUtc);
        orderConfiguration.Ignore(b => b.DomainEvents);
        orderConfiguration.OwnsOne(b => b.OrderNumber, orderNumber =>
            {
                orderNumber.Property(o => o.Value)
                           .IsRequired()
                           .HasColumnName("OrderNumber");
            });
    }
}