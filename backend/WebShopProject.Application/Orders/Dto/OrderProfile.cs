using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Entities;
using WebShopProject.Application.OrderItems.Dto;
using WebShopProject.Application.Orders.Commands.CreateOrderCommand;
using WebShopProject.Application.Orders.Commands.UpdateOrderCommand;

namespace WebShopProject.Application.Orders.Dto
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {

            CreateMap<Order, OrderDto>();
            CreateMap<CreateOrderCommand, Order>();
            CreateMap<UpdateOrderCommand, Order>();

        }
    }
}
