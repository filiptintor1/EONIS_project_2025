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

namespace WebShopProject.Application.Admins.Commands.CreateAdmin
{
    public class CreateAdminCommandHandler(IMapper mapper, IAdminsRepository adminsRepository) : IRequestHandler<CreateAdminCommand, Guid>
    {
        public async Task<Guid> Handle(CreateAdminCommand request, CancellationToken cancellationToken)
        {
            string normalizedUsername = request.Username.ToLowerInvariant();

            var existingAdmin = await adminsRepository.GetAdminByUsername(normalizedUsername);
            if (existingAdmin == null)
            {
                var admin = mapper.Map<Admin>(request);
                Guid id = await adminsRepository.CreateAdmin(admin);
                return id;
            }
            else
            {
                throw new UserAlreadyExistsException(normalizedUsername);
            }
        }
    }
}
