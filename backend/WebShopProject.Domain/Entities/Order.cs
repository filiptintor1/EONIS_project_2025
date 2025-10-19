using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Domain.Entities
{
    public class Order
    {
        public Guid OrderId { get; set; }

        public DateTime Date { get; set; }

        public bool isPaid { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }

        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();


    }
}
