using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopProject.Application.Admins.Dto;

namespace WebShopProject.Application.Admins.Queries.GetAdminById
{
    public class GetAdminByIdQuery(Guid Id) : IRequest<AdminDto>
    {
        public Guid Id { get; set; } = Id;
    }
}
