using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopProject.Application.OrderItems.Dto;
using WebShopProject.Domain.Repositories;

namespace WebShopProject.Application.OrderItems.Queries.GetOrderItemsByOrderAndProduct
{
    public class GetOrderItemsByOrderAndProductQuery : IRequest<OrderItemDto>
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }


        public GetOrderItemsByOrderAndProductQuery(Guid orderId, Guid productId)
        {
            OrderId = orderId;
            ProductId = productId;
        }
    }
}

