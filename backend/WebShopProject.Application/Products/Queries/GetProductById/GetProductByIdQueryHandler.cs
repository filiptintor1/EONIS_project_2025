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

namespace WebShopProject.Application.Products.Queries.GetProductById
{
    internal class GetProductByIdQueryHandler(IMapper mapper, IProductsRepository productsRepository) : IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await productsRepository.GetProductById(request.Id)
                ?? throw new NotFoundException(nameof(Product), request.Id.ToString());
            var productDto = mapper.Map<ProductDto>(product);
            return productDto;
        }
    }
}
