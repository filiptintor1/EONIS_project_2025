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

namespace WebShopProject.Application.OrderItems.Commands.DeleteOrderItemCommand
{
    public class DeleteOrderItemCommandHandler : IRequestHandler<DeleteOrderItemCommand>
    {
        private readonly IOrderItemsRepository _orderItemRepository;
        private readonly IProductsRepository _productsRepository;

        public DeleteOrderItemCommandHandler(IOrderItemsRepository orderItemRepository, IProductsRepository productsRepository)
        {
            _orderItemRepository = orderItemRepository;
            _productsRepository = productsRepository;
        }

        public async Task Handle(DeleteOrderItemCommand request, CancellationToken cancellationToken)
        {
            var orderItem = await _orderItemRepository.GetOrderItemByOrderAndProduct(request.OrderId, request.ProductId);
            if (orderItem == null)
            {
                throw new NotFoundException(nameof(OrderItem), $"OrderId: {request.OrderId}, ProductId: {request.ProductId}");
            }

            var product = await _productsRepository.GetProductById(orderItem.ProductId);
            if (product == null)
            {
                throw new NotFoundException(nameof(Product), orderItem.ProductId.ToString());
            }

            product.Quantity += orderItem.Quantity;
            await _productsRepository.SaveChanges();

            await _orderItemRepository.DeleteOrderItem(orderItem);

            
        }
    }
}
