using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GymSystemBLL.Models.MeemberShipModels;
using GymSystemDAL.Entities;

namespace GymSystemBLL.Mapping
{
    public class MemberShipProfile : Profile
    {
        public MemberShipProfile()
        {
            CreateMap<MemberShipModelView,MemberShip>().ReverseMap();
        }
    }
}