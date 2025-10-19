using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopProject.Application.Admins.Dto
{
    public class AdminDto
    {
        public Guid AdminId { get; set; }
        public string Username { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Surname { get; set; } = default!;
        public string Contact { get; set; } = default!;
    }
}
