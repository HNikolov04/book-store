using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);

        builder.Property(o => o.CustomerName)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(o => o.Address)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(o => o.Quantity)
            .IsRequired();

        builder.HasOne<Book>()
            .WithMany()
            .HasForeignKey(r => r.BookId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}