using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Infrastructure.Configurations;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.CustomerName)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(r => r.Content)
            .IsRequired()
            .HasMaxLength(2000);

        builder.HasOne<Book>()
            .WithMany()
            .HasForeignKey(r => r.BookId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}