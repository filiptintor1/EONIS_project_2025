using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopProject.Application.Products.Commands.CreateProduct;
using WebShopProject.Application.Products.Commands.UpdateProduct;
using WebShopProject.Domain.Entities;

namespace WebShopProject.Application.Products.Dto
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductDto, Product>();
            CreateMap<Product, ProductDto>();
            CreateMap<CreateProductCommand, Product>();
            CreateMap<UpdateProductCommand, Product>();

        }
    }
}
