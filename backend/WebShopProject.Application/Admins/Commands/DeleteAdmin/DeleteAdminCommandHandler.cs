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

namespace WebShopProject.Application.Admins.Commands.DeleteAdmin
{
    public class DeleteAdminCommandHandler(IMapper mapper, IAdminsRepository adminsRepository) : IRequestHandler<DeleteAdminCommand>
    {
        public async Task Handle(DeleteAdminCommand request, CancellationToken cancellationToken)
        {
            var admin = await adminsRepository.GetAdminById(request.Id);
            if (admin is null)
            {
                throw new NotFoundException(nameof(Admin), request.Id.ToString());
            }
            await adminsRepository.DeleteAdmin(admin);
        }
    }
}
