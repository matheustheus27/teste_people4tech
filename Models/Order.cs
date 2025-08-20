using System.ComponentModel.DataAnnotations;

namespace teste_people4tech.Models;

public class Order
{
    public int Id { get; set; }
    [Required, MaxLength(200)] public string CustomerName {get; set;} = string.Empty;
    public DateTime CreatedAt {get; set;}
    public decimal TotalAmount {get; set;}
    public List<OrderItem> Items {get; set;} = new();
}