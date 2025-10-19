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
    internal class GetProductsByCategoryQueryHandler(IMapper mapper, IProductsRepository productsRepository) : IRequestHandler<GetProductsByCategoryQuery, IEnumerable<ProductDto>>
    {
        public async Task<IEnumerable<ProductDto>> Handle(GetProductsByCategoryQuery request, CancellationToken cancellationToken)
        {
            var p = await productsRepository.GetProductsByCategory(request.Category)
                ?? throw new NotFoundException(nameof(Product), request.Category.ToString());

            var productsDtos = mapper.Map<IEnumerable<ProductDto>>(p);
            return productsDtos;
        }
    }
}
