namespace BookStore.Application.DTOs;

public record OrderDto
{
    public int Id { get; set; } 
    public string CustomerName { get; set; }
    public string Address { get; set; }
    public int BookId { get; set; }
    public int Quantity { get; set; }
}