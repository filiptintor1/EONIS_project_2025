using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Entities;
using WebShop.Domain.Exceptions;
using WebShopProject.Application.Orders.Dto;
using WebShopProject.Domain.Repositories;

namespace WebShopProject.Application.Orders.Queries.GetOrderById
{
    public class GetOrderByIdQueryHandler(
    IOrdersRepository ordersRepository,
    IMapper mapper) : IRequestHandler<GetOrderByIdQuery, OrderDto>
    {
        public async Task<OrderDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await ordersRepository.GetOrderById(request.Id)
                ?? throw new NotFoundException(nameof(Order), request.Id.ToString());
            var orderDto = mapper.Map<OrderDto>(order);
            return orderDto;
        }
    }
}
