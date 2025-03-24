using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;
using BookStore.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Repositories;

public class ReviewRepository : IReviewRepository
{
    private readonly IApplicationDbContext _context;

    public ReviewRepository(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Review>> GetAllReviewsAsync()
    {
        return await _context.Reviews.ToListAsync();
    }

    public async Task<Review?> GetReviewByIdAsync(int id)
    {
        return await _context.Reviews.FindAsync(id);
    }

    public async Task<Review> AddReviewAsync(Review review)
    {
        _context.Reviews.Add(review);
        await _context.SaveChangesAsync();
        return review;
    }

    public async Task<Review> UpdateReviewAsync(Review review)
    {
        _context.Reviews.Update(review);
        await _context.SaveChangesAsync();
        return review;
    }

    public async Task<bool> DeleteReviewAsync(int id)
    {
        var review = await _context.Reviews.FindAsync(id);

        if (review == null)
        {
            return false;
        }

        _context.Reviews.Remove(review);
        await _context.SaveChangesAsync();
        return true;
    }
}