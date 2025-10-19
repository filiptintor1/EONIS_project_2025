using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopProject.Application.OrderItems.Dto;

namespace WebShopProject.Application.OrderItems.Commands.CreateOrderItemCommand
{
    public class CreateOrderItemCommand : IRequest<OrderItemDto>
    {
        public Guid ProductId { get; set; }
        public Guid OrderId { get; set; }
        public int Quantity { get; set; }
    }
}
