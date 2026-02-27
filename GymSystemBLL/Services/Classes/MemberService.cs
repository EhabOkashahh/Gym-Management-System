using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using AutoMapper;
using GymSystem.DAL.Entities;
using GymSystemBLL.Models;
using GymSystemBLL.Models.MemberModels;
using GymSystemBLL.Models.PlanModels;
using GymSystemBLL.Services.Interfaces;
using GymSystemDAL.Entities;
using GymSystemDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using GymSystemDAL.Entities.Enums;

namespace GymSystemBLL.Services.Classes
{
    public class MemberService(IUnitOfWork _UnitOfWork, IMapper _autoMapper , IPlanService _planService) : IMemberService
    {
        
        public async Task<IEnumerable<MemberModelView>> GetAllMembersAsync()
        {
           var Members = await GetRepo().GetAllAsync();
           return _autoMapper.Map<IEnumerable<MemberModelView>>(Members);
        }
        

        public async Task<MemberDetailsModelView?> GetMemberByIdAsync(int? id)
        {
            if(id is null ) return null;
            
            var Member = await GetRepo().GetByIdAsync(id.Value);
            if(Member is null) return null!;

            return _autoMapper.Map<MemberDetailsModelView>(Member);
        }

        public async Task<bool> CreateMemberAsync(CreateMemberModelView modelView)
        {
            var res = await FindByEmailOrPhone(modelView.Phone , modelView.Email);
            if(res) return false;

            var Member = _autoMapper.Map<Member>(modelView);

            if(modelView.PlanID is null) return false;

            var plan = await _planService.GetPlanDetails(modelView.PlanID);
            

            var MemberShipStartDate = DateTime.Now;

            var MemberShip = new MemberShip
            {
                CreatedAt = MemberShipStartDate,
                EndDate = MemberShipStartDate.AddDays(plan!.DurationDays - 1),
                UpdatedAt = DateTime.Now,
                StartDate = DateTime.Now,
                MemberShipStatus = MemberShipStatus.Active,
                PlanID = plan.Id
            };

            Member.MemberShip = MemberShip;
            Member.MemberShipID = MemberShip.Id;

            await GetRepo().AddAsync(Member);
            
            return await _UnitOfWork.ApplyToDataBaseAsync() > 0;
        }

        public async Task<bool> DeleteMember(int? id)
        {
            if(id is null) return false;

            var user = await GetRepo().GetByIdAsync(id.Value);
            if(user is null) return false;

            await GetRepo().Delete(id.Value);
            return await _UnitOfWork.ApplyToDataBaseAsync() > 0;

        }
        public async Task<bool> UpdateMember(int id , UpdateMemberModelView model)
        {
            var member = await GetRepo().GetByIdAsync(id);
            if(member is null) return false;

            var UpdatedMember =_autoMapper.Map(model,member);

            UpdatedMember.MemberShip.PlanID = model.PlanID;
            GetRepo().Update(UpdatedMember);

            return await _UnitOfWork.ApplyToDataBaseAsync() > 0;
        }

        public async Task<HealthRecordModelView?> GetHealthRecordDetails(int? id)
        {
            if(id is null) return null;

            var member = await GetRepo().GetByIdAsync(id.Value);
            if(member is null || member.healthRecord is null) return null;

            return _autoMapper.Map<HealthRecordModelView>(member.healthRecord);
        }



        private async Task<bool> FindByEmailOrPhone(string phone , string email)
        {
            var Member = await GetRepo().GetAllAsync();
            return Member.Any(m => m.Phone == phone || m.Email == email);
        }

        private IGenericRepository<Member> GetRepo()
        {
            return _UnitOfWork.GenerateRepository<Member>();
        }
    }
}