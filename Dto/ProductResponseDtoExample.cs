using Swashbuckle.AspNetCore.Filters;

public class ProductResponseDtoExample : IExamplesProvider<ProductResponseDto>
{
    public ProductResponseDto GetExamples()
    {
        return new ProductResponseDto
        {
            Id = 1,
            Name = "Mouse Gamer RGB",
            Description = "Mouse ergonômico com iluminação RGB",
            Price = 149.99M,
            StockQuantity = 25
        };
    }
}
