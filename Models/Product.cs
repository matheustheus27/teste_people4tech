using System.ComponentModel.DataAnnotations;

namespace teste_people4tech.Models;

public class Product
{
    public int Id {get; set;}
    [Required, MaxLength(120)] public string Name {get; set;} = string.Empty;
    [MaxLength(500)] public string? Description {get; set;}
    [Range(0, double.MaxValue)] public decimal Price {get; set;}
    [Range(0, int.MaxValue)] public int StockQuantity {get; set;}
}