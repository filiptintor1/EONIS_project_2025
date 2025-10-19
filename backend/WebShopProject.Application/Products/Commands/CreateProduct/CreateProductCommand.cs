using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopProject.Application.Products.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<Guid>
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Category { get; set; } = default!;
        public int Quantity { get; set; }
        public string? Image { get; set; }
        public string KindOfHoney { get; set; } = default!;
        public decimal Price { get; set; }

    }
}
