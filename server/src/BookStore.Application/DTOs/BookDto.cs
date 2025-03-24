namespace BookStore.Application.DTOs;

public record BookDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string ShortDescription { get; set; }
    public string FullDescription { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }
    public int PurchaseCount { get; set; } = 0;
}