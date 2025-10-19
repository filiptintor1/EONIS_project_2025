using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopProject.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest
    {
        public Guid UserId { get; set; }
        public string Name { get; set; } = default!;
        public string Surname { get; set; } = default!;
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? PostalCode { get; set; }
        public string? Contact { get; set; }
    }
}
