using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Entities;
using WebShop.Domain.Exceptions;
using WebShopProject.Domain.Exceptions;
using WebShopProject.Domain.Repositories;

namespace WebShopProject.Application.Users.Commands.CreateUser
{
    internal class CreateUserCommandHandler(IMapper mapper, IUsersRepository usersRepository) : IRequestHandler<CreateUserCommand, Guid>
    {
        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            string normalizedUsername = request.Username.ToLowerInvariant();

            var userExist = await usersRepository.GetUserByUsername(normalizedUsername);
            if (userExist == null)
            {
                var user = mapper.Map<User>(request);
                user.Username = normalizedUsername;
                Guid id = await usersRepository.CreateUser(user);
                return id;
            }
            else
            {
                throw new UserAlreadyExistsException(normalizedUsername);
            }

        }
    }
}
