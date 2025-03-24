namespace BookStore.Domain.Entities;

public class Order
{
    public int Id { get; set; }
    public string CustomerName { get; set; }
    public string Address { get; set; }
    public int BookId { get; set; }
    public int Quantity { get; set; }
}