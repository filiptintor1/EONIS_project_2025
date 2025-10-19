using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopProject.Application.Orders.Dto;

namespace WebShopProject.Application.Orders.Queries.GetAllOrders
{
    public class GetAllOrdersQuery : IRequest<IEnumerable<OrderDto>>
    {
    }
}
