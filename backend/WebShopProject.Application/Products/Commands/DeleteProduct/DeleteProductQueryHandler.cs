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

namespace WebShopProject.Application.Products.Commands.DeleteProduct
{
    internal class DeleteProductQueryHandler(IMapper mapper, IProductsRepository productsRepository) : IRequestHandler<DeleteProductCommand>
    {
        public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var p = await productsRepository.GetProductById(request.Id);
            if (p is null)
            {
                throw new NotFoundException(nameof(Product), request.Id.ToString());
            }
            await productsRepository.DeleteProduct(p);
        }
    }
}
