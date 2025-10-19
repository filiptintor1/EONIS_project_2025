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

namespace WebShopProject.Application.Orders.Commands.CreateOrderCommand
{
    public class CreateOrderCommandHandler(IMapper mapper, IUsersRepository usersRepository,
    IOrdersRepository ordersRepository,
    IProductsRepository productsRepository,
    IOrderItemsRepository orderItemRepository) : IRequestHandler<CreateOrderCommand, Guid>
    {
        public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var user = await usersRepository.GetUserById(request.UserId);
            if (user is null)
            {
                throw new NotFoundException(nameof(User), request.UserId.ToString());
            }

            var order = new Order
            {
                OrderId = Guid.NewGuid(),
                UserId = request.UserId,
                Date = DateTime.Now,
                isPaid = request.isPaid,
            };


            await ordersRepository.CreateOrder(order);

            return order.OrderId;

        }
    }
}
