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

namespace WebShopProject.Application.Admins.Commands.UpdateAdmin
{
    public class UpdateAdminCommandHandler(IMapper mapper, IAdminsRepository adminsRepository) : IRequestHandler<UpdateAdminCommand>
    {
        public async Task Handle(UpdateAdminCommand request, CancellationToken cancellationToken)
        {
            var admin = await adminsRepository.GetAdminById(request.AdminId);
            if (admin is null)
            {
                throw new NotFoundException(nameof(Admin), request.AdminId.ToString());

            }
            mapper.Map(request, admin);

            await adminsRepository.SaveChanges();
        }
    }
    }
