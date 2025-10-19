using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopProject.Application.Products.Dto;

namespace WebShopProject.Application.Products.Queries.GetProductsByCategory
{
    public class GetProductsByKindQuery : IRequest<IEnumerable<ProductDto>>
    {
        public string Gender { get; init; }

        public GetProductsByKindQuery(string gender)
        {
            Gender = gender;
        }
    }
}
