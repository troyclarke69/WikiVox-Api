using AutoMapper;
using System.Collections.Generic;
using Wikivox_Api.Dtos;
using Wikivox_Api.Models;

namespace Wikivox_Api.Profiles
{
    public class CommandsProfile : Profile
    {
        public CommandsProfile()
        {
            // Source -> Target

            CreateMap<Order, OrderReadDto>();
            CreateMap<List<Order>, OrderReadDto>(); // this was tested for GetAllOrdersByCustomerId -- doesn't quite work

            CreateMap<Contact, ContactReadDto>();
            CreateMap<ContactCreateDto, Contact>();
        }
    }
}