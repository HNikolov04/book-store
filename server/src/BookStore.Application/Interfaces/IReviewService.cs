using BookStore.Application.DTOs;

namespace BookStore.Application.Interfaces;

public interface IReviewService
{
    Task<IEnumerable<ReviewDto>> GetAllReviewsAsync();
    Task<ReviewDto?> GetReviewByIdAsync(int id);
    Task<ReviewDto> CreateReviewAsync(ReviewDto reviewDto);
    Task<ReviewDto?> UpdateReviewAsync(int id, ReviewDto reviewDto);
    Task<bool> DeleteReviewAsync(int id);
}