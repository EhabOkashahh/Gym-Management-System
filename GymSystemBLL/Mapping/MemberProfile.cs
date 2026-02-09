using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GymSystem.DAL.Entities;
using GymSystemBLL.Models;
using GymSystemBLL.Models.MeemberShipModels;
using GymSystemBLL.Models.MemberModels;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Data.SqlClient;

namespace GymSystemBLL.Mapping
{
    public class MemberProfile : Profile
    {
        public MemberProfile()
        {
            CreateMap<CreateMemberModelView, Member>()
            .ForMember(dest => dest.Photo, opt => opt.MapFrom( src => src.Photo))
            .ForMember(dest => dest.Address , opt => opt.MapFrom(src => src.AddressModel))
            .ForMember(dest => dest.healthRecord , opt => opt.MapFrom(src => src.HealthRecordModelView));
            

            CreateMap<Member , MemberModelView>().ReverseMap();

            CreateMap<Member, MemberDetailsModelView>().ForMember(dest => dest.Address, opt => opt.MapFrom(src => $"{src.Address.BuildingNo}-{src.Address.Street}-{src.Address.City}"))
                                                       .ForMember(dest => dest.MemberShip , opt => opt.MapFrom(src => src.MemberShip))
                                                       .ForMember(dest => dest.Address , opt => opt.MapFrom(src => src.Address));


            CreateMap<Address,AddressModelView>().ReverseMap();
            CreateMap<HealthRecord,HealthRecordModelView>().ReverseMap();

        }
    }
}   