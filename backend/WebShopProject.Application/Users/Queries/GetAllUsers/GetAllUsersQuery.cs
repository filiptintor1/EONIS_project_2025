using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopProject.Application.Users.Dto;

namespace WebShopProject.Application.Users.Queries.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<IEnumerable<UserDto>>
    {
    }
}
