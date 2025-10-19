using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopProject.Application.Orders.Dto;

namespace WebShopProject.Application.Orders.Queries.GetOrderByUser
{
    public class GetOrderByUserQuery : IRequest<IEnumerable<OrderDto>>
    {
        public GetOrderByUserQuery(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; }
    }
}
