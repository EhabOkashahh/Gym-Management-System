using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using GymSystem.DAL.Entities;
using GymSystemBLL.Models;
using GymSystemBLL.Services.Interfaces;
using GymSystemDAL.Entities;
using GymSystemDAL.Repositories.Classes;
using GymSystemDAL.Repositories.Interfaces;

namespace GymSystemBLL.Services.Classes
{
    public class AnalyticsService(IUnitOfWork _unitOfWork) : IAnalyticsService
    {
        public async Task<AnalyticsModelView> GetAnalyticsData()
        {
            var allMembers = await _unitOfWork.GenerateRepository<Member>().GetAllAsync();
            var allSessions = await _unitOfWork.GenerateRepository<Session>().GetAllAsync();
            var allTrainers = await _unitOfWork.GenerateRepository<Trainer>().GetAllAsync();

            var now = DateTime.Now;

            return new AnalyticsModelView()
            {
                TotalMembers = allMembers.Count(),
                ActiveMembers = allMembers.Count(m => !m.IsDeleted),

                CompletedSessions = allSessions.Count(s => s.EndDate < now),
                OngoingSessions = allSessions.Count(s => s.StartDate <= now && s.EndDate >= now),
                UpcomingSessions = allSessions.Count(s => s.StartDate > now),

                Trainers = allTrainers.Count()
            };
        }
    }
}