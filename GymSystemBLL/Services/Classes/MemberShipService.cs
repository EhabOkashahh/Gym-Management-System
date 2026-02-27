using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Execution;
using GymSystemBLL.Models.MeemberShipModels;
using GymSystemBLL.Services.Interfaces;
using GymSystemDAL.Data.Contexts;
using GymSystemDAL.Entities;
using GymSystemDAL.Entities.Enums;
using GymSystemDAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace GymSystemBLL.Services.Classes
{
    public class MemberShipService(IUnitOfWork _UnitOfWork, IMapper _mapper , IPlanService _planService) : IMemberShipService
    {
        
        public async Task<IEnumerable<MemberShipModelView>> GetAllMemberShipAsync() => _mapper.Map<IEnumerable<MemberShipModelView>>(await GetRepo().GetAllAsync());
        

        public async Task<MemberShipModelView> GetMemberShipDetails(int Id) {

            var memberShip = _mapper.Map<MemberShipModelView>(await GetRepo().GetByIdAsync(Id));
            var plan = await _planService.GetPlanDetails(memberShip.PlanID);
            memberShip.Plan = plan!;
            return memberShip;
        }

        public async Task<bool> UpdateMemberShip(int id, MemberShipModelView model)
        {
            var MemberShip = await GetRepo().GetByIdAsync(id);
            if(MemberShip is null) return false;

            var UpdatedMember =_mapper.Map(model,MemberShip);
            GetRepo().Update(UpdatedMember);

            return await _UnitOfWork.ApplyToDataBaseAsync() > 0;
        }

        public async Task<bool> RenewMemberShip(int id, int months = 1)
        {
            var membership =  await GetRepo().GetByIdAsync(id);
            if(membership == null || membership.MemberShipStatus == MemberShipStatus.Canceled) return false;

            if(membership.EndDate > DateTime.Now) membership.EndDate = membership.EndDate.AddDays(membership.Plan.DurationDays);
            else
            {
                membership.StartDate = DateTime.Now;
                membership.EndDate = membership.CreatedAt.AddDays(membership.Plan.DurationDays);
            }
            
            membership.MemberShipStatus = MemberShipStatus.Active;

            return await _UnitOfWork.ApplyToDataBaseAsync() > 0;
        }

        public async Task<bool> CancelMemberShip(int id)
        {
            var membership = await GetRepo().GetByIdAsync(id);
            if (membership == null || membership.MemberShipStatus == MemberShipStatus.Canceled)
            return false;

            membership.MemberShipStatus = MemberShipStatus.Canceled;
            
            return await _UnitOfWork.ApplyToDataBaseAsync() > 0 ;
        }

        public async Task<IEnumerable<MemberShip>> GetActiveMemberships()
        {
            return  GetRepo().GetAllAsync().Result.Where(ms => ms.MemberShipStatus == MemberShipStatus.Active);
        }

        private IGenericRepository<MemberShip> GetRepo()
        {
            return  _UnitOfWork.GenerateRepository<MemberShip>();
        }
    }
}