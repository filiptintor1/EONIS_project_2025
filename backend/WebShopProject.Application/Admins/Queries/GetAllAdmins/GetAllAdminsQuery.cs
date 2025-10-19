using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopProject.Application.Admins.Dto;

namespace WebShopProject.Application.Admins.Queries.GetAllAdmins
{
    public class GetAllAdminsQuery: IRequest<IEnumerable<AdminDto>>
    {
    }
}
