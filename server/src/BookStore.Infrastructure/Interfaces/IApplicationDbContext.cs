using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Book> Books { get; set; }
    DbSet<Order> Orders { get; set; }
    DbSet<Review> Reviews { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}