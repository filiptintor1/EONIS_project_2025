using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopProject.Application.OrderItems.Dto;
using WebShopProject.Application.OrderItems.Queries.GetOrderItemsByOrderId;
using WebShopProject.Domain.Repositories;

namespace WebShopProject.Application.OrderItems.Queries.GetOrderItemsByOrderAndProduct
{
    public class GetOrderItemsByOrderAndProductQueryHandler : IRequestHandler<GetOrderItemsByOrderAndProductQuery, OrderItemDto>
    {
        private readonly IOrderItemsRepository _orderItemsRepository;
        private readonly IMapper _mapper;

        public GetOrderItemsByOrderAndProductQueryHandler(IOrderItemsRepository orderItemsRepository, IMapper mapper)
        {
            _orderItemsRepository = orderItemsRepository;
            _mapper = mapper;
        }
        public async Task<OrderItemDto> Handle(GetOrderItemsByOrderAndProductQuery request, CancellationToken cancellationToken)
        {
            var orderItems = await _orderItemsRepository.GetOrderItemByOrderAndProduct(request.OrderId, request.ProductId);

            var orderItemDto = _mapper.Map<OrderItemDto>(orderItems);

            return orderItemDto;
        }
    }
}
