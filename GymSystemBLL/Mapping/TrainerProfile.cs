using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GymSystem.DAL.Entities;
using GymSystemBLL.Models.TrainerModels;

namespace GymSystemBLL.Mapping
{
    public class TrainerProfile : Profile
    {
        public TrainerProfile()
        {
            CreateMap<Trainer, TrainerModelView>()
            .ForMember(dest => dest.Specialization,
                opt => opt.MapFrom(src => src.Specialities.ToString()));


           CreateMap<Trainer, TrainerDetailsModelView>()
            .ForMember(dest => dest.Specialization,
                opt => opt.MapFrom(src => src.Specialities))
            .ForMember(dest => dest.Address,
                opt => opt.MapFrom(src => src.Address));


            CreateMap<Trainer, UpdateTrainerModelView>()
            .ForMember(dest => dest.Specialization,
                opt => opt.MapFrom(src => src.Specialities))
            .ForMember(dest => dest.Address,
                opt => opt.MapFrom(src => src.Address)).ReverseMap();

            CreateMap<CreateTrainerModelView, Trainer>()
            .ForMember(dest => dest.Specialities,
                opt => opt.MapFrom(src => src.Specialization))
            .ForMember(dest => dest.Address,
                opt => opt.MapFrom(src => src.Address));


            CreateMap<TrainerDetailsModelView , UpdateTrainerModelView>().ReverseMap();
        }
    }
}