using Swashbuckle.AspNetCore.Filters;

public class OrderResponseDtoListExample : IExamplesProvider<List<OrderResponseDto>>
{
    public List<OrderResponseDto> GetExamples()
    {
        return new List<OrderResponseDto>
        {
            new OrderResponseDtoExample().GetExamples(), // usa o exemplo anterior
            new OrderResponseDto
            {
                Id = 2,
                CustomerName = "Maria Oliveira",
                CreatedAt = DateTime.UtcNow,
                TotalAmount = 200.00M,
                Items = new List<OrderItemResponseDto>
                {
                    new OrderItemResponseDto
                    {
                        ProductId = 2,
                        ProductName = "Monitor 24''",
                        Quantity = 1,
                        UnitPrice = 200.00M
                    }
                }
            }
        };
    }
}
