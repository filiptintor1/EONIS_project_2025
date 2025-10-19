using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Exceptions;
using WebShop.Domain.Repositories;
using WebShopProject.Domain.Entities;

namespace WebShopProject.Application.Products.Commands.UpdateProduct
{
    internal class UpdateProductCommandHandler(IMapper mapper, IProductsRepository productsRepository) : IRequestHandler<UpdateProductCommand>
    {
        public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        { 
            var p = await productsRepository.GetProductById(request.ProductId);
            if (p is null)
            {
                throw new NotFoundException(nameof(Product), request.ProductId.ToString());

            }
            mapper.Map(request, p);

            await productsRepository.SaveChanges();
        }
    }
}
