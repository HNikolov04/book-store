namespace BookStore.Domain.Entities;

public class Review
{
    public int Id { get; set; }
    public string CustomerName { get; set; }
    public string Content { get; set; }
    public int BookId { get; set; }
}