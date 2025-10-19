using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopProject.Application.Users.Dto;
using WebShopProject.Domain.Repositories;

namespace WebShopProject.Application.Users.Queries.GetAllUsers
{
    public class GetAllUsersQueryHandler(IUsersRepository usersRepository,
    IMapper mapper) : IRequestHandler<GetAllUsersQuery, IEnumerable<UserDto>>
    {
        public async Task<IEnumerable<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await usersRepository.GetAllUsers();
            var usersDtos = mapper.Map<IEnumerable<UserDto>>(users);
            return usersDtos;
        }
    }
}
