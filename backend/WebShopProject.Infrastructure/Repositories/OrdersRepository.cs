using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Entities;
using WebShopProject.Domain.Repositories;
using WebShopProject.Infrastructure.Database;

namespace WebShopProject.Infrastructure.Repositories
{
    internal class OrdersRepository(ProjectDbContext dbContext) : IOrdersRepository
    {
        public async Task<Guid> CreateOrder(Order order)
        {
            dbContext.Orders.Add(order);
            await dbContext.SaveChangesAsync();
            return order.OrderId;
        }

        public async Task DeleteOrder(Order order)
        {
            dbContext.Remove(order);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            var orders = await dbContext.Orders
           .Include(x => x.OrderItems).ToListAsync();
            return orders;
        }

        public async Task<Order?> GetOrderById(Guid orderId)
        {
            var order = await dbContext.Orders
           .Include(x => x.OrderItems)
           .FirstOrDefaultAsync(x => x.OrderId == orderId);
            return order;
        }

        public async Task<IEnumerable<Order>> GetOrderByUserId(Guid userId)
        {
            var orders = await dbContext.Orders.Include(x => x.OrderItems)
            .Where(o => userId == o.UserId).ToListAsync();
            return orders;
        }

        public async Task SaveChanges()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
