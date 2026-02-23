using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GymSystem.DAL.Entities;
using GymSystemBLL.Models.PlanModels;

namespace GymSystemBLL.Mapping
{
    public class PlanProfile : Profile
    {
        public PlanProfile()
        {
            CreateMap<Plan, PlanModelView>().ForMember(dest => dest.MemberShip , opt => opt.MapFrom(src => src.MemberShips)).ReverseMap();
            CreateMap<Plan, UpdatePlanModelView>().ReverseMap();
            CreateMap<PlanModelView, UpdatePlanModelView>().ReverseMap();
        }
    }
}