using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Entities;

namespace WebShopProject.Application.Orders.Dto
{
    public class OrderDto
    {
        public Guid OrderId { get; set; }

        public DateTime Date { get; set; }

        public bool isPaid { get; set; }

        public Guid UserId { get; set; }

    }
}
