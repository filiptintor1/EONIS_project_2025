using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopProject.Application.Products.Dto;

namespace WebShopProject.Application.Products.Queries.GetProductById
{
    public class GetProductByIdQuery : IRequest<ProductDto>
    {
        public GetProductByIdQuery(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; }
    }
}
