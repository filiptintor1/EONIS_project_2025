using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopProject.Domain.Entities;

namespace WebShop.Domain.Entities
{
    public class OrderItem
    {
        public Guid ProductId { get; set; }
        public Guid OrderId { get; set; }
        public int Quantity { get; set; }
        public Order Order { get; set; } = null!;
        public Product Product { get; set; } = null!;

    }
}
