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
using GymSystemDAL.Repositories.Interfaces;
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

        private IGenericRepository<MemberShip> GetRepo()
        {
            return  _UnitOfWork.GenerateRepository<MemberShip>();
        }
    }
}