using System.ComponentModel.DataAnnotations;

namespace teste_people4tech.Dto;

public class CreateOrderDto
{
    [Required]
    public string CustomerName {get; set;} = null!;

    [Required]
    public List<OrderItemRequestDto> Items {get; set;} = new List<OrderItemRequestDto>();
}
