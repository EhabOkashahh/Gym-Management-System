using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GymSystem.DAL.Entities;
using GymSystemBLL.Models;
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
        
            CreateMap<Address,AddressModelView>().ReverseMap();
            CreateMap<HealthRecord,HealthRecordModelView>().ReverseMap();

        }
    }
}   