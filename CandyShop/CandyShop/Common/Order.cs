namespace CandyShop.Common;

public class Order
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public List<OrderItem> Items { get; set; }
    public decimal TotalAmount { get; set; }
}

public class OrderItem
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
