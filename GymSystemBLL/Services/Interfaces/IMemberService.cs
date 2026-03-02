using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymSystem.DAL.Entities;
using GymSystemBLL.Models;
using GymSystemBLL.Models.MemberModels;

namespace GymSystemBLL.Services.Interfaces
{
    public interface IMemberService
    {
        Task<IEnumerable<MemberModelView>> GetAllMembersAsync();
        Task<MemberDetailsModelView?> GetMemberByIdAsync(int? id);
        Task<HealthRecordModelView?> GetHealthRecordDetails(int? id);
        Task<bool> CreateMemberAsync(CreateMemberModelView Model);
        Task<bool> UpdateMember(int id , UpdateMemberModelView model);
        Task<bool> RestoreMember(int id);
        Task<bool> SoftDeleteMember(int? id);
    }
}