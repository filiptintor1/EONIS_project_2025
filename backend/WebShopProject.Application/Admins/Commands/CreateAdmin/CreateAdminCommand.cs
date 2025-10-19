using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopProject.Application.Admins.Commands.CreateAdmin
{
    public class CreateAdminCommand : IRequest<Guid>
    {
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Surname { get; set; } = default!;
        public string Contact { get; set; } = default!;
    }
}
