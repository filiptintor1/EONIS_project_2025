using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Entities;
using WebShopProject.Application.OrderItems.Dto;

namespace WebShopProject.Application.OrderItems.Queries.GetOrderItemsByOrderId
{
    public class GetOrderItemsByOrderIdQuery : IRequest<IEnumerable<OrderItemDto>>
    {
        public Guid OrderId { get; set; }

        public GetOrderItemsByOrderIdQuery(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}
