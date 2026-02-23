using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GymSystem.DAL.Entities;
using GymSystemBLL.Models.PlanModels;
using GymSystemBLL.Services.Interfaces;
using GymSystemDAL.Repositories.Classes;
using GymSystemDAL.Repositories.Interfaces;

namespace GymSystemBLL.Services.Classes
{
    public class PlanService(IUnitOfWork _UnitOfWork, IMapper _mapper) : IPlanService
    {
        public async Task<IEnumerable<PlanModelView>> GetAllPlans()
        {
            var Plans = await GetRepo().GetAllAsync();
            return _mapper.Map<IEnumerable<PlanModelView>>(Plans);
        }
        

        public async Task<PlanModelView?> GetPlanDetails(int? id)
        {
            if(id is null) return null!;
            var Plan = await GetRepo().GetByIdAsync(id.Value);

            if(Plan is null) return null!;
            var mappedVersion = _mapper.Map<PlanModelView>(Plan);
            return mappedVersion;
        }

        public async Task<bool> UpdatePlanData(int id,UpdatePlanModelView model)
        {
            var plan = await GetRepo().GetByIdAsync(id);
             if(plan is null || plan.MemberShips.Any(ms => ms.status.Equals("Expired",StringComparison.OrdinalIgnoreCase))) return false;

            GetRepo().Update(_mapper.Map(model,plan)); 
            return await _UnitOfWork.ApplyToDataBaseAsync() > 0;
        }

        public async Task<bool> TogglePlanActiveStatus(int id)
        {
            var plan = await GetRepo().GetByIdAsync(id);
             if(plan is null || plan.MemberShips.Any(ms => ms.status.Equals("Active",StringComparison.OrdinalIgnoreCase))) return false;

            plan.IsActive = !plan.IsActive;
            return await _UnitOfWork.ApplyToDataBaseAsync() > 0;
        }

        private IGenericRepository<Plan> GetRepo()
        {
            return  _UnitOfWork.GenerateRepository<Plan>();
        }
    }
}