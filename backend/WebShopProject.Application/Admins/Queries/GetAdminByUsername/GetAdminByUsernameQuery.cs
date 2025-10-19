using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopProject.Application.Admins.Dto;

namespace WebShopProject.Application.Admins.Queries.GetAdminByUsername
{
    public class GetAdminByUsernameQuery(string Username): IRequest<AdminDto>
    {
        public string Username { get; set; } = Username;
    }
}
