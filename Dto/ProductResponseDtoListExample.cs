using Swashbuckle.AspNetCore.Filters;

public class ProductResponseDtoListExample : IExamplesProvider<List<ProductResponseDto>>
{
    public List<ProductResponseDto> GetExamples()
    {
        return new List<ProductResponseDto>
        {
            new ProductResponseDto
            {
                Id = 1,
                Name = "Mouse Gamer RGB",
                Description = "Mouse ergonômico com iluminação RGB",
                Price = 149.99M,
                StockQuantity = 25
            },
            new ProductResponseDto
            {
                Id = 2,
                Name = "Teclado Mecânico",
                Description = "Teclado mecânico com switches vermelhos",
                Price = 299.99M,
                StockQuantity = 15
            }
        };
    }
}
