using BookStore.Domain.Entities;

namespace BookStore.Domain.Interfaces;

public interface IReviewRepository
{
    Task<IEnumerable<Review>> GetAllReviewsAsync();
    Task<Review?> GetReviewByIdAsync(int id);
    Task<Review> AddReviewAsync(Review review);
    Task<Review> UpdateReviewAsync(Review review);
    Task<bool> DeleteReviewAsync(int id);
}