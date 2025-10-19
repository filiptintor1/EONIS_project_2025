using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Entities;

namespace WebShopProject.Domain.Entities
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Category { get; set; } = default!;
        public string KindOfHoney { get; set; } = default!;
        public int Quantity { get; set; }
        public string? Image { get; set; }
        public decimal Price { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    }
}
