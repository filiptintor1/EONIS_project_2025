using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopProject.Application.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<Guid>
    {
        public string Name { get; set; } = default!;
        public string Surname { get; set; } = default!;
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? PostalCode { get; set; }
        public string? Contact { get; set; }
    }
}
