using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Exceptions;
using WebShop.Domain.Repositories;
using WebShopProject.Application.Products.Dto;
using WebShopProject.Domain.Entities;

namespace WebShopProject.Application.Products.Queries.GetProductsByCategory
{
    internal class GetProductsByKindQueryHandler(IMapper mapper, IProductsRepository productsRepository) : IRequestHandler<GetProductsByKindQuery, IEnumerable<ProductDto>>
    {
        public async Task<IEnumerable<ProductDto>> Handle(GetProductsByKindQuery request, CancellationToken cancellationToken)
        {
            var products = await productsRepository.GetProductsByKind(request.Gender)
                ?? throw new NotFoundException(nameof(Product), request.Gender.ToString());

            var productsDtos = mapper.Map<IEnumerable<ProductDto>>(products);
            return productsDtos;
        }
    }
}
