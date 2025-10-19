using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopProject.Application.Products.Dto;

namespace WebShopProject.Application.Products.Queries.GetProductsByCategory
{
    public class GetProductsByCategoryQuery : IRequest<IEnumerable<ProductDto>>
    {
        public GetProductsByCategoryQuery(string category)
        {
            this.Category = category;
        }

        public string Category { get; }
    }
}
