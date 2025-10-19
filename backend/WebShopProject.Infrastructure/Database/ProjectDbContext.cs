using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Entities;
using WebShopProject.Domain.Entities;

namespace WebShopProject.Infrastructure.Database
{
    public class ProjectDbContext : DbContext
    {
        public ProjectDbContext(DbContextOptions<ProjectDbContext> options) : base(options)
        {
        }
        internal DbSet<User> Users { get; set; }
        internal DbSet<Admin> Admins { get; set; }
        internal DbSet<Order> Orders { get; set; }
        internal DbSet<Product> Products { get; set; }
        internal DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // order user relationship
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasMany(u => u.Orders)
                      .WithOne(o => o.User)
                      .HasForeignKey(o => o.UserId);

                entity.OwnsOne(u => u.Address);
            });

            // Configure Order and OrderItem relationship
            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId);

            // Configure Product and OrderItem relationship
            modelBuilder.Entity<Product>()
                .HasMany(p => p.OrderItems)
                .WithOne(oi => oi.Product)
                .HasForeignKey(oi => oi.ProductId);

            // Composite key for OrderItem
            modelBuilder.Entity<OrderItem>()
                .HasKey(oi => new { oi.OrderId, oi.ProductId }); // Composite primary key
        }
    }
}
