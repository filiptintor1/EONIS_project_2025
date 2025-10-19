using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopProject.Application.PaginationHelper;
using WebShopProject.Application.Products.Dto;
using WebShopProject.Domain.Entities;

namespace WebShopProject.Application.Products.Queries.GetAllProducts
{
    public class GetAllProductsQuery : IRequest<PagedResult>
    {
        public string? SearchPhrase { get; set; }
        public string? Category { get; set; }
        public string? KindOfHoney { get; set; }

        public string? SortBy { get; set; }
        public SortDirection SortDirection { get; set; }
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;
    }
}
