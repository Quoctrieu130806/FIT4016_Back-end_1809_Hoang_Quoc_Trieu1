using Microsoft.EntityFrameworkCore;
using OrderManagementApp.Models;

public class OrderDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Chú ý: Thay đổi connection string cho phù hợp với SQL Server của bạn
        optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=OrderManagement;Trusted_Connection=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Fluent API cho Unique constraints
        modelBuilder.Entity<Product>().HasIndex(p => p.Name).IsUnique();
        modelBuilder.Entity<Product>().HasIndex(p => p.Sku).IsUnique();
        modelBuilder.Entity<Order>().HasIndex(o => o.OrderNumber).IsUnique();
        modelBuilder.Entity<Order>().HasIndex(o => o.CustomerEmail).IsUnique();

        // Seed Data: 15 Products
        for (int i = 1; i <= 15; i++)
        {
            modelBuilder.Entity<Product>().HasData(new Product { 
                Id = i, Name = $"Laptop Type {i}", Sku = $"SKU-00{i}", 
                Price = 1000 + i, StockQuantity = 50, Category = "Electronics" 
            });
        }

        // Seed Data: 30 Orders
        for (int i = 1; i <= 30; i++)
        {
            modelBuilder.Entity<Order>().HasData(new Order { 
                Id = i, ProductId = (i % 15) + 1, OrderNumber = $"ORD-20260117-{i:D4}", 
                CustomerName = $"Customer {i}", CustomerEmail = $"client{i}@gmail.com", 
                Quantity = 1, OrderDate = DateTime.Now.AddDays(-1)
            });
        }
    }
}