using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopProject.Application.Orders.Dto;

namespace WebShopProject.Application.Orders.Queries.GetOrderById
{
    public class GetOrderByIdQuery : IRequest<OrderDto>
    {
        public GetOrderByIdQuery(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; }
    }
}
