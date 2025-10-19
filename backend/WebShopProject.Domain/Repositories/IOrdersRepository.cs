using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Entities;

namespace WebShopProject.Domain.Repositories
{
    public interface IOrdersRepository
    {
        Task<Guid> CreateOrder(Order order);
        Task<IEnumerable<Order>> GetOrderByUserId(Guid userId);
        Task SaveChanges();
        Task DeleteOrder(Order order);
        Task<Order?> GetOrderById(Guid orderId);
        Task<IEnumerable<Order>> GetAllOrders();
    }
}
