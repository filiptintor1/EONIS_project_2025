using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Entities;

namespace WebShopProject.Domain.Repositories
{
    public interface IOrderItemsRepository
    {
        Task CreateOrderItem(OrderItem oi);
        Task<IEnumerable<OrderItem>> GetAllOrderItems();
        Task SaveChanges();
        Task DeleteOrderItem(OrderItem oi);
        Task<IEnumerable<OrderItem>> GetOrderItemsByOrderId(Guid orderId);

        Task<OrderItem?> GetOrderItemByOrderAndProduct(Guid orderId, Guid productId);

    }
}
