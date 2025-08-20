public class OrderResponseDto
{
    public int Id {get; set;}
    public string CustomerName {get; set;} = null!;
    public DateTime CreatedAt {get; set;}
    public decimal TotalAmount {get; set;}
    public List<OrderItemResponseDto> Items {get; set;} = new();
}

public class OrderItemResponseDto
{
    public int ProductId {get; set;}
    public string ProductName {get; set;} = null!;
    public int Quantity {get; set;}
    public decimal UnitPrice {get; set;}
}
