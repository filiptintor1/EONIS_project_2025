using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Entities;
using WebShopProject.Application.OrderItems.Commands.CreateOrderItemCommand;
using WebShopProject.Application.OrderItems.Commands.UpdateOrderItemCommand;
using WebShopProject.Application.Products.Dto;
using WebShopProject.Domain.Entities;

namespace WebShopProject.Application.OrderItems.Dto
{
    public class OrderItemProfile : Profile
    {
        public OrderItemProfile()
        {
            CreateMap<OrderItem, OrderItemDto>()
                .ReverseMap();
            CreateMap<UpdateOrderItemCommand, OrderItem>().ReverseMap();
            CreateMap<CreateOrderItemCommand, OrderItem>().ReverseMap();
            CreateMap<Product, ProductDto>();

        }
    }
}
