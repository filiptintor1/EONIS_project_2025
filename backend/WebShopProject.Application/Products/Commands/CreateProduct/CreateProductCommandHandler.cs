using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Repositories;
using WebShopProject.Domain.Entities;

namespace WebShopProject.Application.Products.Commands.CreateProduct
{
    internal class CreateProductCommandHandler(IMapper mapper, IProductsRepository productsRepository) : IRequestHandler<CreateProductCommand, Guid>
    {
        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            Product product = mapper.Map<Product>(request);
            Guid id = await productsRepository.CreateProduct(product);
            return id;
        }
    }
}
