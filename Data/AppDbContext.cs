using Microsoft.EntityFrameworkCore;
using teste_people4tech.Models;

namespace teste_people4tech.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    // Tabelas
    public DbSet<Product> Products {get; set;} = null!;
    public DbSet<Order> Orders {get; set;} = null!;
    public DbSet<OrderItem> OrderItems {get; set;} = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>()
            .Property(p => p.Name)
            .HasMaxLength(120)
            .IsRequired();

        modelBuilder.Entity<Product>()
            .Property(p => p.Price)
            .HasColumnType("decimal(10,2)");

        modelBuilder.Entity<Order>()
            .Property(o => o.CustomerName)
            .HasMaxLength(200)
            .IsRequired();

        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Product)
            .WithMany()
            .HasForeignKey(oi => oi.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Order)
            .WithMany(o => o.Items)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
