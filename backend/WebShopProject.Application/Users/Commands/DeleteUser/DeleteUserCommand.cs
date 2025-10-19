using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopProject.Application.Users.Commands.DeleteUser
{
    public class DeleteUserCommand(Guid Id) : IRequest
    {
        public Guid Id { get; set; } = Id;
    }
}
