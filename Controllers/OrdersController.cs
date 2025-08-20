using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Filters;
using teste_people4tech.Data;
using teste_people4tech.Models;
using teste_people4tech.Dto;
using System.Data;

namespace teste_people4tech.Controllers;

[ApiController]
[Route("orders")]
public class OrdersController : ControllerBase
{
    private readonly AppDbContext _db;
    public OrdersController(AppDbContext db) => _db = db;

    /// <summary>
    /// Cria um novo pedido.
    /// </summary>
    /// <param name="data">Dados do pedido, incluindo nome do cliente e itens.</param>
    /// <returns>Retorna o pedido criado com todos os detalhes.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(OrderResponseDto), StatusCodes.Status201Created)]
    [SwaggerRequestExample(typeof(CreateOrderDto), typeof(CreateOrderDtoExample))]
    [SwaggerResponseExample(201, typeof(OrderResponseDtoExample))]
    public async Task<IActionResult> Create(CreateOrderDto data)
    {
        var productIds = data.Items.Select(i => i.ProductId).Distinct().ToList();

        var products = await _db.Products.Where(p => productIds.Contains(p.Id)).ToDictionaryAsync(p => p.Id);

        var notFound = productIds.Where(id => !products.ContainsKey(id)).ToList();
        if (notFound.Count > 0)
        {
            return BadRequest(new { message = "Produtos inexistentes", ids = notFound });
        }

        var insufficient = new List<object>();
        foreach (var i in data.Items)
        {
            var p = products[i.ProductId];

            if (p.StockQuantity < i.Quantity)
            {
                insufficient.Add(new { productId = p.Id, disponivel = p.StockQuantity, solicitado = i.Quantity });
            }
        }

        if (insufficient.Count > 0)
        {
            return BadRequest(new { message = "Estoque insuficiente", details = insufficient });
        }

        var order = new Order
        {
            CustomerName = data.CustomerName.Trim(),
            CreatedAt = DateTime.UtcNow,
            Items = new List<OrderItem>()
        };

        decimal total = 0;

        foreach (var i in data.Items)
        {
            var p = products[i.ProductId];

            order.Items.Add(new OrderItem
            {
                ProductId = p.Id,
                Quantity = i.Quantity,
                UnitPrice = p.Price
            });

            total += p.Price * i.Quantity;
        }

        order.TotalAmount = total;

        _db.Orders.Add(order);
        await _db.SaveChangesAsync();

        var createdOrder = await _db.Orders.Include(o => o.Items).ThenInclude(i => i.Product).FirstAsync(o => o.Id == order.Id);

        return CreatedAtAction(nameof(GetById), new { id = createdOrder.Id }, createdOrder);
    }

    /// <summary>
    /// Retorna todos os pedidos.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<OrderResponseDto>), StatusCodes.Status200OK)]
    [SwaggerResponseExample(200, typeof(OrderResponseDtoListExample))]
    public async Task<IActionResult> GetAll()
    {
        var orders = await _db.Orders.Include(o => o.Items).ThenInclude(i => i.Product).ToListAsync();

        var response = orders.Select(o => new OrderResponseDto
        {
            Id = o.Id,
            CustomerName = o.CustomerName,
            CreatedAt = o.CreatedAt,
            TotalAmount = o.TotalAmount,
            Items = o.Items.Select(i => new OrderItemResponseDto
            {
                ProductId = i.ProductId,
                Quantity = i.Quantity
            }).ToList()
        }).ToList();

        return Ok(response);
    }


    /// <summary>
    /// Retorna um pedido pelo id.
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(OrderResponseDto), StatusCodes.Status200OK)]
    [SwaggerResponseExample(200, typeof(OrderResponseDtoExample))]
    public async Task<IActionResult> GetById(int id)
    {
        var order = await _db.Orders.Include(o => o.Items).ThenInclude(i => i.Product).FirstOrDefaultAsync(o => o.Id == id);

        if (order == null)
        {
            return NotFound();
        }

        var response = new OrderResponseDto
        {
            Id = order.Id,
            CustomerName = order.CustomerName,
            CreatedAt = order.CreatedAt,
            TotalAmount = order.TotalAmount,
            Items = order.Items.Select(i => new OrderItemResponseDto
            {
                ProductId = i.ProductId,
                Quantity = i.Quantity
            }).ToList()
        };

        return Ok(response);
    }
}