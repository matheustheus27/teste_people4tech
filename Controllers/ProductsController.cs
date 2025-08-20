using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Filters;
using teste_people4tech.Data;
using teste_people4tech.Models;
using teste_people4tech.Dto;

namespace teste_people4tech.Controllers;

[ApiController]
[Route("products")]
public class ProductsController : ControllerBase
{
    private readonly AppDbContext _db;
    public ProductsController(AppDbContext db) => _db = db;

    /// <summary>
    /// Cria um novo produto.
    /// </summary>
    /// <param name="data">Dados do produto</param>
    /// <returns>Produto criado</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ProductResponseDto), StatusCodes.Status201Created)]
    [SwaggerRequestExample(typeof(CreateProductDto), typeof(CreateProductDtoExample))]
    [ProducesResponseType(typeof(ProductResponseDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create(CreateProductDto data)
    {
        var product = new Product
        {
            Name = data.Name.Trim(),
            Description = data.Description?.Trim(),
            Price = data.Price,
            StockQuantity = data.StockQuantity
        };

        _db.Products.Add(product);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }

    /// <summary>
    /// Retorna todos os produtos.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<ProductResponseDto>), StatusCodes.Status200OK)]
    [SwaggerResponseExample(200, typeof(ProductResponseDtoListExample))]
    public async Task<IActionResult> GetAll()
    {
        var products = await _db.Products.AsNoTracking().ToListAsync();

        var response = products.Select(p => new ProductResponseDto
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            StockQuantity = p.StockQuantity
        }).ToList();

        return Ok(response);
    }

    /// <summary>
    /// Retorna um produto pelo id.
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ProductResponseDto), StatusCodes.Status200OK)]
    [SwaggerResponseExample(200, typeof(ProductResponseDtoExample))]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _db.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);

        if (product is null)
        {
            return NotFound();
        }

        var response = new ProductResponseDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            StockQuantity = product.StockQuantity
        };

        return Ok(response);
    }
}
