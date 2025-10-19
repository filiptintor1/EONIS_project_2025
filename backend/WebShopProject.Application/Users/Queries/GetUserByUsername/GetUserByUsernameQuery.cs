using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopProject.Application.Users.Dto;

namespace WebShopProject.Application.Users.Queries.GetUserByUsername
{
    public class GetUserByUsernameQuery(string Username) : IRequest<UserDto>
    {
        public string Username = Username;
    }
}
