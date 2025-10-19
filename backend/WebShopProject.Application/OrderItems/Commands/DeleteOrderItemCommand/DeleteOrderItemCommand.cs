using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopProject.Application.OrderItems.Commands.DeleteOrderItemCommand
{
    public class DeleteOrderItemCommand : IRequest
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }

        public DeleteOrderItemCommand(Guid orderId, Guid productId)
        {
            OrderId = orderId;
            ProductId = productId;
        }
    }
}
