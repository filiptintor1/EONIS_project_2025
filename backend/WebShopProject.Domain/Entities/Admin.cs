using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Domain.Entities
{
    public class Admin
    {
        public Guid AdminId { get; set; }
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string Salt { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Surname { get; set; } = default!;
        public string Contact { get; set; } = default!;

    }
}
