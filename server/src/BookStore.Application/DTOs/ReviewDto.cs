namespace BookStore.Application.DTOs;

public record ReviewDto
{
    public int Id { get; set; } 
    public string CustomerName { get; set; }
    public string Content { get; set; } 
    public int BookId { get; set; }
}