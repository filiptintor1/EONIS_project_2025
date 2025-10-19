using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Entities;
using WebShop.Domain.Exceptions;
using WebShopProject.Application.Users.Dto;
using WebShopProject.Domain.Repositories;

namespace WebShopProject.Application.Users.Queries.GetUserByUsername
{
    public class GetUserByUsernameQueryHandler(IUsersRepository usersRepository,
    IMapper mapper) : IRequestHandler<GetUserByUsernameQuery, UserDto>
    {
        public async Task<UserDto> Handle(GetUserByUsernameQuery request, CancellationToken cancellationToken)
        {
            var user = await usersRepository.GetUserByUsername(request.Username)
                ?? throw new NotFoundException(nameof(User), request.Username.ToString());
            var userDto = mapper.Map<UserDto>(user);
            return userDto;
        }
    }
}
