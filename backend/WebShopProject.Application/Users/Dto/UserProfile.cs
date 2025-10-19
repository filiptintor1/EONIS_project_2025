using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Entities;
using WebShopProject.Application.Users.Commands.CreateUser;
using WebShopProject.Application.Users.Commands.UpdateUser;

namespace WebShopProject.Application.Users.Dto
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>()
            .ForMember(d => d.City, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.City))
            .ForMember(d => d.Street, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.Street))
            .ForMember(d => d.PostalCode, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.PostalCode));

            CreateMap<CreateUserCommand, User>()
                .ForMember(d => d.Address, opt => opt.MapFrom(
                    src => new Address
                    {
                        City = src.City,
                        PostalCode = src.PostalCode,
                        Street = src.Street
                    }));

            CreateMap<UpdateUserCommand, User>().ForMember(d => d.Address, opt => opt.MapFrom(
                    src => new Address
                    {
                        City = src.City,
                        PostalCode = src.PostalCode,
                        Street = src.Street
                    })).ReverseMap();
        }
    }
}
