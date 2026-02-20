using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GymSystem.DAL.Entities;
using GymSystemBLL.Models;

namespace GymSystemBLL.Mapping
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<Address,AddressModelView>().ReverseMap();
        }
    }
}