using System.ComponentModel.DataAnnotations;

namespace teste_people4tech.Dto;

public class CreateProductDto
{
    [Required]
    public string Name {get; set;} = null!;
    public string? Description {get; set;}
    [Required]
    public decimal Price {get; set;}
    [Required]
    public int StockQuantity {get; set;}
}
