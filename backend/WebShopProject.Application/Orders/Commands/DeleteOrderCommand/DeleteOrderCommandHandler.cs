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

namespace WebShopProject.Application.Orders.Commands.DeleteOrderCommand
{
    public class DeleteOrderCommandHandler(IMapper mapper, IOrdersRepository ordersRepository, IProductsRepository productsRepository, IOrderItemsRepository orderItemsRepository) : IRequestHandler<DeleteOrderCommand>
    {
        public async Task Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await ordersRepository.GetOrderById(request.OrderId);
            if (order is null)
            {
                throw new NotFoundException(nameof(Order), request.OrderId.ToString());
            }

            var orderItems = await orderItemsRepository.GetOrderItemsByOrderId(request.OrderId);
            foreach (var orderItem in orderItems)
            {
                var product = await productsRepository.GetProductById(orderItem.ProductId);
                if (product == null)
                {
                    throw new NotFoundException(nameof(Product), orderItem.ProductId.ToString());
                }

                product.Quantity += orderItem.Quantity;
                await productsRepository.SaveChanges();

                await orderItemsRepository.DeleteOrderItem(orderItem);
            }

            await ordersRepository.DeleteOrder(order);
        }
    }
}
