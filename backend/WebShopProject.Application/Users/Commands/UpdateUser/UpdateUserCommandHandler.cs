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

namespace WebShopProject.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler(IMapper mapper,
    IUsersRepository usersRepository) : IRequestHandler<UpdateUserCommand>
    {
        public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await usersRepository.GetUserById(request.UserId);
            if (user is null)
            {
                throw new NotFoundException(nameof(User), request.UserId.ToString());

            }
            mapper.Map(request, user);

            await usersRepository.SaveChanges();

        }
    }
}
