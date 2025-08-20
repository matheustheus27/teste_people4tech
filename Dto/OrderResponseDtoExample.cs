using Swashbuckle.AspNetCore.Filters;

public class OrderResponseDtoExample : IExamplesProvider<OrderResponseDto>
{
    public OrderResponseDto GetExamples()
    {
        return new OrderResponseDto
        {
            Id = 1,
            CustomerName = "João da Silva",
            CreatedAt = DateTime.UtcNow,
            TotalAmount = 350.50M,
            Items = new List<OrderItemResponseDto>
            {
                new OrderItemResponseDto
                {
                    ProductId = 1,
                    ProductName = "Mouse Gamer RGB",
                    Quantity = 2,
                    UnitPrice = 150.00M
                },
                new OrderItemResponseDto
                {
                    ProductId = 3,
                    ProductName = "Teclado Mecânico",
                    Quantity = 1,
                    UnitPrice = 50.50M
                }
            }
        };
    }
}
