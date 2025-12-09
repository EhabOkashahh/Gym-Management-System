using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using AutoMapper;
using GymSystem.DAL.Entities;
using GymSystemBLL.Models;
using GymSystemBLL.Services.Interfaces;
using GymSystemDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GymSystemBLL.Services.Classes
{
    public class MemberService(IUnitOfWork _UnitOfWork, IMapper _autoMapper) : IMemberService
    {
        
        public async Task<IEnumerable<MemberModelView>> GetAllMembersAsync()
        {
           var Members = await GetRepo().GetAllAsync();
           return _autoMapper.Map<IEnumerable<MemberModelView>>(Members);
        }
        

        public async Task<MemberModelView?> GetMemberByIdAsync(int? id)
        {
            if(id is null ) return null;
            
            var Member = await GetRepo().GetByIdAsync(id.Value);
            if(Member is null) return null!;

            return _autoMapper.Map<MemberModelView>(Member);
        }

        public async Task<bool> CreateMemberAsync(CreateMemberModelView modelView)
        {
            var res = await FindByEmailOrPhone(modelView.Phone , modelView.Email);
            if(res) return false;

            var Member = _autoMapper.Map<Member>(modelView);
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