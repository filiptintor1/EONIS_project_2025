using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Entities;
using WebShop.Domain.Exceptions;
using WebShopProject.Domain.Repositories;

namespace WebShopProject.Application.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler(IMapper mapper, IUsersRepository usersRepository) : IRequestHandler<DeleteUserCommand>
    {
        public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await usersRepository.GetUserById(request.Id);
            if (user is null)
            {
                throw new NotFoundException(nameof(User), request.Id.ToString());
            }
            await usersRepository.DeleteUser(user);

        }
    }
}
