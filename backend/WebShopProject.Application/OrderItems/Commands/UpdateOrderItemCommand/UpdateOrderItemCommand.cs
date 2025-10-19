using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopProject.Application.OrderItems.Commands.UpdateOrderItemCommand
{
    public class UpdateOrderItemCommand : IRequest
    {
        public Guid ProductId { get; set; }
        public Guid OrderId { get; set; }
        public int Quantity { get; set; }
    }
}
