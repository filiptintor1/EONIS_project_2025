using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopProject.Application.Orders.Commands.CreateOrderCommand
{
    public class CreateOrderCommand : IRequest<Guid>
    {

        public bool isPaid { get; set; }

        public Guid UserId { get; set; }
    }
}
