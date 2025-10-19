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

namespace WebShopProject.Application.Users.Queries.GetUserById
{
    internal class GetUserByIdQueryHandler(IUsersRepository usersRepository,
    IMapper mapper) : IRequestHandler<GetUserByIdQuery, UserDto>
    {
        public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await usersRepository.GetUserById(request.Id)
                ?? throw new NotFoundException(nameof(User), request.Id.ToString());
            var userDto = mapper.Map<UserDto>(user);
            return userDto;
        }
    }
}
