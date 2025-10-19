using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopProject.Application.Admins.Commands.DeleteAdmin
{
    public class DeleteAdminCommand(Guid Id) : IRequest
    {
        public Guid Id { get; set; } = Id;
    }
}
