using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Entities;
using WebShop.Domain.Exceptions;
using WebShop.Domain.Repositories;
using WebShopProject.Application.OrderItems.Dto;
using WebShopProject.Domain.Entities;
using WebShopProject.Domain.Repositories;

namespace WebShopProject.Application.OrderItems.Commands.CreateOrderItemCommand
{
    public class CreateOrderItemCommandHandler(IMapper mapper, IOrderItemsRepository orderItemRepository,
   IOrdersRepository ordersRepository,
   IProductsRepository productsRepository) : IRequestHandler<CreateOrderItemCommand, OrderItemDto>
    {
        public async Task<OrderItemDto> Handle(CreateOrderItemCommand request, CancellationToken cancellationToken)
        {
            var order = await ordersRepository.GetOrderById(request.OrderId);
            if (order is null)
            {
                throw new NotFoundException(nameof(Order), request.OrderId.ToString());
            }



            var product = await productsRepository.GetProductById(request.ProductId);
            if (product is null)
            {
                throw new NotFoundException(nameof(Product), request.OrderId.ToString());
            }



            if (request.Quantity > product.Quantity)
            {
                throw new InvalidOperationException($"Not enough stock for Product ID {request.ProductId}. Requested: {request.Quantity}, Available: {product.Quantity}");
            }
            else
            {
                product.Quantity = product.Quantity - request.Quantity;
                await productsRepository.SaveChanges();
                var oi = mapper.Map<OrderItem>(request);
                await orderItemRepository.CreateOrderItem(oi);
                return mapper.Map<OrderItemDto>(oi);
            }

        }
    }

}
