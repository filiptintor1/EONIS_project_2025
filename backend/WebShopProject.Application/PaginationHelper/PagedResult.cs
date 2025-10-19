using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopProject.Application.Products.Dto;

namespace WebShopProject.Application.PaginationHelper
{
    public class PagedResult
    {

        public PagedResult(IEnumerable<ProductDto> items, int totalCount, int pageSize, int pageNumber)
        {
            Items = items;
            TotalItemsCount = totalCount;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public IEnumerable<ProductDto> Items { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalItemsCount { get; set; }
    }
}
