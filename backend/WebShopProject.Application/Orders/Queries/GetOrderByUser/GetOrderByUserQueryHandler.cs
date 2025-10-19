using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebShop.Domain.Entities;
using WebShop.Domain.Exceptions;
using WebShopProject.Application.Orders.Dto;
using WebShopProject.Domain.Repositories;

namespace WebShopProject.Application.Orders.Queries.GetOrderByUser
{
    public class GetOrderByUserQueryHandler(IMapper mapper, IOrdersRepository ordersRepository) : IRequestHandler<GetOrderByUserQuery, IEnumerable<OrderDto>>
    {
        public async Task<IEnumerable<OrderDto>> Handle(GetOrderByUserQuery request, CancellationToken cancellationToken)
        {
            var orders = await ordersRepository.GetOrderByUserId(request.UserId)
                         ?? throw new NotFoundException(nameof(Order), request.UserId.ToString());

            // Sort orders by date (assuming there's a Date property)
            var sortedOrders = orders.OrderByDescending(o => o.Date);

            // Map the sorted orders to DTOs
            var orderDto = mapper.Map<IEnumerable<OrderDto>>(sortedOrders);
            return orderDto;
        }
    }
}