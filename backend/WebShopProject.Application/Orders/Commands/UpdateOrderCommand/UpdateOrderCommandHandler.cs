using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Entities;
using WebShop.Domain.Exceptions;
using WebShopProject.Domain.Repositories;

namespace WebShopProject.Application.Orders.Commands.UpdateOrderCommand
{
    public class UpdateOrderCommandHandler(IMapper mapper,
    IOrdersRepository ordersRepository) : IRequestHandler<UpdateOrderCommand>
    {
        public async Task Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {

            var order = await ordersRepository.GetOrderById(request.OrderId);
            if (order is null)
            {
                throw new NotFoundException(nameof(User), request.OrderId.ToString());

            }
            mapper.Map(request, order);

            await ordersRepository.SaveChanges();
        }
    }
}
