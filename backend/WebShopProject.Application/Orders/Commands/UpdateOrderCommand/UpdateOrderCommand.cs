using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopProject.Application.Orders.Commands.UpdateOrderCommand
{
    public class UpdateOrderCommand : IRequest
    {
        public Guid OrderId { get; set; }
        public bool IsPaid { get; set; }
    }
}
