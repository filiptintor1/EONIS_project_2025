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

namespace WebShopProject.Application.Admins.Queries.GetAdminById
{
    public class GetAdminByIdQueryHandler(IMapper mapper, IAdminsRepository adminsRepository) : IRequestHandler<GetAdminByIdQuery, AdminDto>
    {
        public async Task<AdminDto> Handle(GetAdminByIdQuery request, CancellationToken cancellationToken)
        {
            var admin = await adminsRepository.GetAdminById(request.Id)
                 ?? throw new NotFoundException(nameof(User), request.Id.ToString());
            var adminDto = mapper.Map<AdminDto>(admin);
            return adminDto;
        }
    }
}
