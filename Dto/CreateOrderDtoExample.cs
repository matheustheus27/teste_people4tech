using Swashbuckle.AspNetCore.Filters;
using teste_people4tech.Dto;

public class CreateOrderDtoExample : IExamplesProvider<CreateOrderDto>
{
    public CreateOrderDto GetExamples()
    {
        return new CreateOrderDto
        {
            CustomerName = "João da Silva",
            Items = new List<OrderItemRequestDto>
            {
                new OrderItemRequestDto {ProductId = 1, Quantity = 2}
            }
        };
    }
}
