using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Entities;

namespace WebShopProject.Application.Users.Dto
{
    public class UserDto
    {
        public Guid UserId { get; set; }
        public string Name { get; set; } = default!;
        public string Surname { get; set; } = default!;
        public string Username { get; set; } = default!;
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? PostalCode { get; set; }
        public string? Contact { get; set; }
    }
}
