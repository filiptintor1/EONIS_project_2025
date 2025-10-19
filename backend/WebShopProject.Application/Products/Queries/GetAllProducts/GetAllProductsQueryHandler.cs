using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Repositories;
using WebShopProject.Application.PaginationHelper;
using WebShopProject.Application.Products.Dto;

namespace WebShopProject.Application.Products.Queries.GetAllProducts
{
    public class GetAllProductsQueryHandler(IProductsRepository productsRepository,
    IMapper mapper) : IRequestHandler<GetAllProductsQuery, PagedResult>
    {
        public async Task<PagedResult> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var (products, totalCount) = await productsRepository.GetAllProducts(request.PageSize,
                request.PageNumber, request.SearchPhrase, request.Category, request.KindOfHoney, request.SortBy, request.SortDirection);

            var productsDtos = mapper.Map<IEnumerable<ProductDto>>(products);
            var result = new PagedResult(productsDtos, totalCount, request.PageSize, request.PageNumber);
            return result;
        }

    }
}
