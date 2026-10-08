using HW.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HW
{
    public class ShopContext : DbContext
    {
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql("Host=localhost;Port=5432;Database=shop_linq;Username=postgres;Password=0000");
        }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<Category>().ToTable("categories");
            mb.Entity<Category>().Property(x => x.Id).HasColumnName("id");
            mb.Entity<Category>().Property(x => x.Name).HasColumnName("name");

            mb.Entity<Customer>().ToTable("customers");
            mb.Entity<Customer>().Property(x => x.Id).HasColumnName("id");
            mb.Entity<Customer>().Property(x => x.FullName).HasColumnName("full_name");
            mb.Entity<Customer>().Property(x => x.Email).HasColumnName("email");
            mb.Entity<Customer>().Property(x => x.City).HasColumnName("city");
            mb.Entity<Customer>().Property(x => x.RegisteredAt).HasColumnName("registered_at");

            mb.Entity<Product>().ToTable("products");
            mb.Entity<Product>().Property(x => x.Id).HasColumnName("id");
            mb.Entity<Product>().Property(x => x.Name).HasColumnName("name");
            mb.Entity<Product>().Property(x => x.CategoryId).HasColumnName("category_id");
            mb.Entity<Product>().Property(x => x.Price).HasColumnName("price");
            mb.Entity<Product>().Property(x => x.Stock).HasColumnName("stock");
            mb.Entity<Product>().HasOne(x => x.Category).WithMany(c => c.Products).HasForeignKey(x => x.CategoryId);

            mb.Entity<Order>().ToTable("orders");
            mb.Entity<Order>().Property(x => x.Id).HasColumnName("id");
            mb.Entity<Order>().Property(x => x.CustomerId).HasColumnName("customer_id");
            mb.Entity<Order>().Property(x => x.OrderDate).HasColumnName("order_date");
            mb.Entity<Order>().Property(x => x.Status).HasColumnName("status");
            mb.Entity<Order>().HasOne(x => x.Customer).WithMany(c => c.Orders).HasForeignKey(x => x.CustomerId);

            mb.Entity<OrderItem>().ToTable("order_items");
            mb.Entity<OrderItem>().HasKey(x => new { x.OrderId, x.ProductId });
            mb.Entity<OrderItem>().Property(x => x.OrderId).HasColumnName("order_id");
            mb.Entity<OrderItem>().Property(x => x.ProductId).HasColumnName("product_id");
            mb.Entity<OrderItem>().Property(x => x.Quantity).HasColumnName("quantity");
            mb.Entity<OrderItem>().Property(x => x.UnitPrice).HasColumnName("unit_price");
            mb.Entity<OrderItem>().HasOne(x => x.Order).WithMany(o => o.Items).HasForeignKey(x => x.OrderId);
            mb.Entity<OrderItem>().HasOne(x => x.Product).WithMany().HasForeignKey(x => x.ProductId);
        }
    }
}
