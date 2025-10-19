using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopProject.Application.OrderItems.Dto;
using WebShopProject.Domain.Repositories;

namespace WebShopProject.Application.OrderItems.Queries.GetOrderItemsByOrderId
{
    public class GetOrderItemsByOrderIdQueryHandler : IRequestHandler<GetOrderItemsByOrderIdQuery, IEnumerable<OrderItemDto>>
    {
        private readonly IOrderItemsRepository _orderItemsRepository;
        private readonly IMapper _mapper;

        public GetOrderItemsByOrderIdQueryHandler(IOrderItemsRepository orderItemsRepository, IMapper mapper)
        {
            _orderItemsRepository = orderItemsRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderItemDto>> Handle(GetOrderItemsByOrderIdQuery request, CancellationToken cancellationToken)
        {
            var orderItems = await _orderItemsRepository.GetOrderItemsByOrderId(request.OrderId);

            var orderItemDtos = _mapper.Map<IEnumerable<OrderItemDto>>(orderItems);

            return orderItemDtos;
        }
    }
}
