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
using WebShopProject.Domain.Entities;
using WebShopProject.Domain.Repositories;

namespace WebShopProject.Application.OrderItems.Commands.UpdateOrderItemCommand
{
    public class UpdateOrderItemCommandHandler( IMapper mapper,
    IOrderItemsRepository orderItemRepository, IProductsRepository productsRepository) : IRequestHandler<UpdateOrderItemCommand>
    {
        public async Task Handle(UpdateOrderItemCommand request, CancellationToken cancellationToken)
        {
            var oi = await orderItemRepository.GetOrderItemByOrderAndProduct(request.OrderId,request.ProductId);
            if (oi is null)
            {
                throw new NotFoundException(nameof(OrderItem), request.OrderId.ToString() +" + " +request.ProductId.ToString());

            }
            var product = await productsRepository.GetProductById(request.ProductId);
            if (product == null)
            {
                throw new NotFoundException(nameof(Product), request.ProductId.ToString());
            }
            if (request.Quantity > product.Quantity)
            {
                throw new InvalidOperationException($"Not enough stock for Product ID {request.ProductId}. Requested: {request.Quantity}, Available: {product.Quantity}");
            }
            product.Quantity += oi.Quantity - request.Quantity;

            mapper.Map(request, oi);

            await productsRepository.SaveChanges();
            await orderItemRepository.SaveChanges();
        }
    }
}
