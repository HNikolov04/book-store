using BookStore.Application.DTOs;
using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;

namespace BookStore.Application.Services;

public class ReviewService : IReviewService
{
    private readonly IReviewRepository _reviewRepository;

    public ReviewService(IReviewRepository reviewRepository)
    {
        _reviewRepository = reviewRepository;
    }

    public async Task<IEnumerable<ReviewDto>> GetAllReviewsAsync()
    {
        var reviews = await _reviewRepository.GetAllReviewsAsync();

        return reviews.Select(review => new ReviewDto
        {
            Id = review.Id,
            CustomerName = review.CustomerName,
            Content = review.Content,
            BookId = review.BookId
        }).ToList();
    }

    public async Task<ReviewDto?> GetReviewByIdAsync(int id)
    {
        var review = await _reviewRepository.GetReviewByIdAsync(id);

        if (review == null)
        {
            return null;
        }

        return new ReviewDto
        {
            Id = review.Id,
            CustomerName = review.CustomerName,
            Content = review.Content,
            BookId = review.BookId
        };
    }

    public async Task<ReviewDto> CreateReviewAsync(ReviewDto reviewDto)
    {
        var review = new Review
        {
            CustomerName = reviewDto.CustomerName,
            Content = reviewDto.Content,
            BookId = reviewDto.BookId
        };

        var newReview = await _reviewRepository.AddReviewAsync(review);

        return new ReviewDto
        {
            Id = newReview.Id,
            CustomerName = newReview.CustomerName,
            Content = newReview.Content,
            BookId = newReview.BookId
        };
    }

    public async Task<ReviewDto?> UpdateReviewAsync(int id, ReviewDto reviewDto)
    {
        var review = await _reviewRepository.GetReviewByIdAsync(id);

        if (review == null)
        {
            return null;
        }

        review.CustomerName = reviewDto.CustomerName;
        review.Content = reviewDto.Content;
        review.BookId = reviewDto.BookId;

        var updatedReview = await _reviewRepository.UpdateReviewAsync(review);

        return new ReviewDto
        {
            Id = updatedReview.Id,
            CustomerName = updatedReview.CustomerName,
            Content = updatedReview.Content,
            BookId = updatedReview.BookId
        };
    }

    public async Task<bool> DeleteReviewAsync(int id)
    {
        return await _reviewRepository.DeleteReviewAsync(id);
    }
}
