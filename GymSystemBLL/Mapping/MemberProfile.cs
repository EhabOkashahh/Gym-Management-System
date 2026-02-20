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
            
            CreateMap<Member,UpdateMemberModelView>().ReverseMap()
                .ForMember(dest => dest.Address , o => o.MapFrom(s => s.AddressModel))
                .ForMember(dest => dest.Phone , o => o.MapFrom(s => s.Phone))
                .ForMember(dest => dest.Email , o => o.MapFrom(s => s.Email))
                .ForMember(dest => dest.Id , o => o.MapFrom(s => s.Id))

                .ForMember(dest => dest.CreatedAt , o => o.Ignore())
                .ForMember(dest => dest.DateOfBirth , o => o.Ignore())
                .ForMember(dest => dest.Gender , o => o.Ignore())
                .ForMember(dest => dest.healthRecord , o => o.Ignore())
                .ForMember(dest => dest.MemberSessions , o => o.Ignore())
                .ForMember(dest => dest.MemberShip , o => o.Ignore())
                .ForMember(dest => dest.MemberShipID , o => o.Ignore())
                .ForMember(dest => dest.Name , o => o.Ignore())
                .ForMember(dest => dest.Photo , o => o.Ignore());


            CreateMap<UpdateMemberModelView,MemberDetailsModelView>()
                .ForMember(d => d.Email, o => o.MapFrom(s => s.Email))
                .ForMember(d => d.Phone, o => o.MapFrom(s => s.Phone))
                .ForMember(d => d.Address, o => o.MapFrom(s => s.AddressModel))
                .ForMember(dest => dest.Id , o => o.MapFrom(s => s.Id))
                .ForMember(dest => dest.Photo , o => o.MapFrom(s => s.Photo))
                .ForMember(dest => dest.Name , o => o.MapFrom(S => S.Name))


                .ForMember(dest => dest.DateOfBirth , o => o.Ignore())
                .ForMember(dest => dest.Gender , o => o.Ignore())
                .ForMember(dest => dest.MemberShip , o => o.Ignore())
                .ForMember(dest => dest.MemberShipID , o => o.Ignore());
                

            CreateMap<MemberDetailsModelView, UpdateMemberModelView>()
                .ForMember(d => d.Email, o => o.MapFrom(s => s.Email))
                .ForMember(d => d.Phone, o => o.MapFrom(s => s.Phone))
                .ForMember(d => d.AddressModel, o => o.MapFrom(s => s.Address))
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.Photo, o => o.MapFrom(s => s.Photo))
                .ForMember(dest => dest.Name , o => o.MapFrom(S => S.Name));

                
        }
    }
}   