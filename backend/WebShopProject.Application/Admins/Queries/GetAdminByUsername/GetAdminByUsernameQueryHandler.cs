using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Entities;
using WebShop.Domain.Exceptions;
using WebShopProject.Application.Admins.Dto;
using WebShopProject.Domain.Repositories;

namespace WebShopProject.Application.Admins.Queries.GetAdminByUsername
{
    public class GetAdminByUsernameQueryHandler(IAdminsRepository adminsRepository,
    IMapper mapper) : IRequestHandler<GetAdminByUsernameQuery, AdminDto>
    {
        public async Task<AdminDto> Handle(GetAdminByUsernameQuery request, CancellationToken cancellationToken)
        {
            var admin = await adminsRepository.GetAdminByUsername(request.Username)
                ?? throw new NotFoundException(nameof(Admin), request.Username.ToString());
            var adminDto = mapper.Map<AdminDto>(admin);
            return adminDto;
        }
    }
}
