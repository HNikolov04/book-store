using BookStore.Application.DTOs;
using BookStore.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReviewsController : ControllerBase
{
    private readonly IReviewService _reviewService;

    public ReviewsController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllReviews()
    {
        var reviews = await _reviewService.GetAllReviewsAsync();

        return Ok(reviews);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetReview(int id)
    {
        var review = await _reviewService.GetReviewByIdAsync(id);

        if (review == null)
        {
            return NotFound();
        }

        return Ok(review);
    }

    [HttpPost]
    public async Task<IActionResult> CreateReview([FromBody] ReviewDto reviewDto)
    {
        var newReview = await _reviewService.CreateReviewAsync(reviewDto);

        return CreatedAtAction(nameof(GetReview), new { id = newReview.Id }, newReview);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrder(int id, [FromBody] ReviewDto reviewDto)
    {
        var updatedOrder = await _reviewService.UpdateReviewAsync(id, reviewDto);

        if (updatedOrder == null)
        {
            return NotFound();
        }

        return Ok(updatedOrder);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReview(int id)
    {
        var result = await _reviewService.DeleteReviewAsync(id);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}