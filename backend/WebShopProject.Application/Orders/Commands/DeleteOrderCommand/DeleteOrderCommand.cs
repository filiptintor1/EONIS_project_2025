using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Entities;

namespace WebShopProject.Application.Orders.Commands.DeleteOrderCommand
{
    public class DeleteOrderCommand : IRequest
    {
        public Guid OrderId { get; set; }

        public DeleteOrderCommand(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}
