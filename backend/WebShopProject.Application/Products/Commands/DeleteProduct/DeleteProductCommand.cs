using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopProject.Application.Products.Commands.DeleteProduct
{
    public class DeleteProductCommand : IRequest
    {
        public Guid Id { get; init; }

        public DeleteProductCommand(Guid id)
        {
            Id = id;
        }
    }
}
