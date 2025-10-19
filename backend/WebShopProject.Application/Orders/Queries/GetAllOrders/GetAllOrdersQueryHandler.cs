using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebShopProject.Application.Orders.Dto;
using WebShopProject.Domain.Repositories;

namespace WebShopProject.Application.Orders.Queries.GetAllOrders
{
    public class GetAllOrdersQueryHandler(IOrdersRepository ordersRepository, IMapper mapper) : IRequestHandler<GetAllOrdersQuery, IEnumerable<OrderDto>>
    {
        public async Task<IEnumerable<OrderDto>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await ordersRepository.GetAllOrders();

            // Sort orders by date (assuming there's a Date property)
            var sortedOrders = orders.OrderByDescending(o => o.Date); 

            // Map the sorted orders to DTOs
            var ordersDtos = mapper.Map<IEnumerable<OrderDto>>(sortedOrders);
            return ordersDtos;
        }
    }
}