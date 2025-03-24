using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Configurations;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Title)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(b => b.ShortDescription)
            .HasMaxLength(500);

        builder.Property(b => b.FullDescription)
            .HasMaxLength(2000);

        builder.Property(b => b.Price)
            .HasPrecision(10, 2);

        builder.Property(b => b.ImageUrl)
            .HasMaxLength(500);
    }
}