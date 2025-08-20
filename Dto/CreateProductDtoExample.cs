using Swashbuckle.AspNetCore.Filters;
using teste_people4tech.Dto;

public class CreateProductDtoExample : IExamplesProvider<CreateProductDto>
{
    public CreateProductDto GetExamples()
    {
        return new CreateProductDto
        {
            Name = "Mouse Gamer",
            Description = "Mouse RGB com sensor de alta precisão",
            Price = 150.99M,
            StockQuantity = 20
        };
    }
}
