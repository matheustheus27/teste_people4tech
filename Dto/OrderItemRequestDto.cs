using System.ComponentModel.DataAnnotations;

namespace teste_people4tech.Dto;

public class OrderItemRequestDto
{
    [Required(ErrorMessage = "ProductId is required")]
    public int ProductId {get; set;}

    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
    public int Quantity {get; set;}
}
