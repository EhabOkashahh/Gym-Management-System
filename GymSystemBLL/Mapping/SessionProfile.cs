using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GymSystem.DAL.Entities;
using GymSystemBLL.Models.SessionModels;
using GymSystemBLL.Models.TrainerModels;

namespace GymSystemBLL.Mapping
{
    public class SessionProfile : Profile
    {
        public SessionProfile()
        {
            // Global conversions
            CreateMap<DateTime, DateOnly>()
                .ConvertUsing(src => DateOnly.FromDateTime(src));

            CreateMap<DateOnly, DateTime>()
                .ConvertUsing(src => src.ToDateTime(TimeOnly.MinValue));

            CreateMap<TimeSpan, TimeOnly>()
                .ConvertUsing(src => TimeOnly.FromTimeSpan(src));

            CreateMap<TimeOnly, TimeSpan>()
                .ConvertUsing(src => src.ToTimeSpan());

            // Create Session
            CreateMap<CreateSessionModelView, Session>().ReverseMap();

            // Update Session
            CreateMap<UpdateSessionModelView, Session>().ReverseMap();
            CreateMap<DetailsSessionModelView, Session>().ReverseMap();
            CreateMap<DetailsSessionModelView, UpdateSessionModelView>().ReverseMap();

            CreateMap<Session, SessionModelView>()
            // Basic fields
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
            .ForMember(dest => dest.TrainerName, opt => opt.MapFrom(src => src.Trainer.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Capacity, opt => opt.MapFrom(src => src.Capacity))
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))

            // Computed fields
            .ForMember(dest => dest.AvailableSlots, opt => opt.MapFrom(src => src.Capacity - src.MemberSessions.Count))
            .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.EndDate - src.StartDate))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src =>
                src.StartDate > DateTime.Now ? "Upcoming" :
                src.EndDate < DateTime.Now ? "Completed" : "Ongoing"))
            .ForMember(dest => dest.DateDisplay, opt => opt.MapFrom(src => src.StartDate.ToString("MMM dd, yyyy")))
            .ForMember(dest => dest.TimeRangeDisplay, opt => opt.MapFrom(src => $"{src.StartDate:hh:mm tt} - {src.EndDate:hh:mm tt}"));
        }        
    }
}