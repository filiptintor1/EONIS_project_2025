using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopProject.Application.Admins.Dto;
using WebShopProject.Domain.Repositories;

namespace WebShopProject.Application.Admins.Queries.GetAllAdmins
{
    internal class GetAllAdminsQueryHandler(IMapper mapper, IAdminsRepository adminsRepository) : IRequestHandler<GetAllAdminsQuery, IEnumerable<AdminDto>>
    {
        public async Task<IEnumerable<AdminDto>> Handle(GetAllAdminsQuery request, CancellationToken cancellationToken)
        {
            var admins = await adminsRepository.GetAllAdmins();
            var adminsDtos = mapper.Map<IEnumerable<AdminDto>>(admins);
            return adminsDtos;
        }
    }
}
